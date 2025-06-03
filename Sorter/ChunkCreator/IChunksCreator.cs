using System.Collections.Concurrent;

namespace Sorter;

public interface IChunksCreator
{
    Task<ConcurrentBag<string>> GetSortedChunksAsync(string inputFile);
}