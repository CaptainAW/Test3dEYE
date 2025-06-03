using System.Collections.Concurrent;
using System.Diagnostics;
using Sorter;

#if DEBUG
args = ["unsorted.txt", "sorted.txt"];
#endif

if (args.Length < 2)
{
    Console.WriteLine("Usage: Sorter <inputFilePath> <outputFilePath>");
    return;
}

var inputPath = args[0];
var outputPath = args[1];

IChunksCreator chunksCreator = new ChunkCreator();
IChunksMerger chunksMerger = new ChunksMerger();

Stopwatch stopWatch = new();
stopWatch.Start();

FileInfo fileInfo = new FileInfo(inputPath);
long sizeInBytes = fileInfo.Length;
Console.WriteLine($"File size: {sizeInBytes} bytes");
Console.WriteLine($"Creating chunks...");
ConcurrentBag<string> tempFiles = await chunksCreator.GetSortedChunksAsync(inputPath);

Console.WriteLine($"Created chunks amount: {tempFiles.Count} Time: {stopWatch.Elapsed}");
Console.WriteLine($"Merging chunks...");

await chunksMerger.MergeSortedFilesAsync(tempFiles, outputPath);

foreach (string file in tempFiles)
    File.Delete(file);

Console.WriteLine($"Complete in: {stopWatch.Elapsed}");
stopWatch.Stop();