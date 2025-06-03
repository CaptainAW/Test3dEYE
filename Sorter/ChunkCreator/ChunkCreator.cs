using System.Collections.Concurrent;
using System.Text;

namespace Sorter;

public class ChunkCreator : IChunksCreator
{
    private readonly LineComparer _comparer = new();
    private readonly SemaphoreSlim _semaphore = new(Environment.ProcessorCount);
    private readonly ConcurrentBag<string> _tempFiles = new();

    public async Task<ConcurrentBag<string>> GetSortedChunksAsync(string inputFile)
    {
        using StreamReader reader = new(inputFile, Encoding.UTF8, false);

        List<Task> chunkTasks = new();
        List<string> currentChunk = new();
        long currentChunkSize = 0;
        long newLineSymbolsSize = Environment.NewLine.Length;

        while (!reader.EndOfStream)
        {
            string? stringLine = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(stringLine)) continue;

            long lineSize = Encoding.UTF8.GetByteCount(stringLine) + newLineSymbolsSize;
            
            if (currentChunkSize + lineSize > Constants.ChunkSizeBytes && currentChunk.Count > 0)
            {
                List<string> chunkCopy = currentChunk;
                chunkTasks.Add(ProcessChunkAsync(chunkCopy));
                currentChunk = [];
                currentChunkSize = 0;
            }

            currentChunk.Add(stringLine);
            currentChunkSize += lineSize;
        }

        if (currentChunk.Count > 0)
            chunkTasks.Add(ProcessChunkAsync(currentChunk));

        await Task.WhenAll(chunkTasks);
        return _tempFiles;
    }

    private async Task ProcessChunkAsync(List<string> chunk)
    {
        await _semaphore.WaitAsync();
        try
        {
            List<Line> lines = new(chunk.Count);
            foreach (string stringLine in chunk)
            {
                if (string.IsNullOrWhiteSpace(stringLine)) continue;
                Line line = LineParser.Parse(stringLine);
                lines.Add(line);
            }

            lines.Sort(_comparer);

            string tempFile = Path.GetTempFileName();
            _tempFiles.Add(tempFile);

            await using StreamWriter writer = new(tempFile, false, Encoding.UTF8);
            foreach (Line line in lines)
                await writer.WriteLineAsync(line.FullString);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}