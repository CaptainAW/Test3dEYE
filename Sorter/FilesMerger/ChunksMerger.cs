using System.Text;

namespace Sorter;

public class ChunksMerger : IChunksMerger
{
    public async Task MergeSortedFilesAsync(IEnumerable<string> sortedFiles, string outputFile)
    {
        List<StreamReader> readers = [];
        readers.AddRange(sortedFiles.Select(file => new StreamReader(file, Encoding.UTF8, false)));
        PriorityQueue<(Line line, int readerIndex), (string phrase, int number, int readerIndex)> queue = new();

        for (int i = 0; i < readers.Count; i++)
        {
            if (readers[i].EndOfStream)
                continue;
            string? stringLine = await readers[i].ReadLineAsync();
            if (stringLine == null)
                continue;
            Line line = LineParser.Parse(stringLine);
            queue.Enqueue((line, i), (line.Phrase, line.Number, i));
        }

        await using StreamWriter writer = new(outputFile, false, Encoding.UTF8);

        while (queue.Count > 0)
        {
            (Line line, int readerIndex) = queue.Dequeue();
            await writer.WriteLineAsync(line.FullString);
            if (readers[readerIndex].EndOfStream)
                continue;
            string? nextStringLine = await readers[readerIndex].ReadLineAsync();
            if (nextStringLine == null)
                continue;
            Line nextLine = LineParser.Parse(nextStringLine);
            queue.Enqueue((nextLine, readerIndex), (nextLine.Phrase, nextLine.Number, readerIndex));
        }

        foreach (StreamReader reader in readers)
            reader.Dispose();
    }
}