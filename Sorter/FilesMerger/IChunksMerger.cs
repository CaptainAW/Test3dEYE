namespace Sorter;

public interface IChunksMerger
{
    Task MergeSortedFilesAsync(IEnumerable<string> sortedFiles, string outputFile);
}