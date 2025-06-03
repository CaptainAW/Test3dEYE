using System.Diagnostics;
using FileGenerator;

#if DEBUG
args = ["unsorted.txt", "100Mb"];
#endif

if (args.Length < 2)
{
    Console.WriteLine("Usage: TestFileGenerator <output_file> <target_size_MB/GB>");
    return;
}

string filePath = args[0];
string fileSizeString = args[1].ToUpper().Trim();

ITextFileGenerator fileGenerator = new TextFileGenerator();
Console.WriteLine($"Creating file...");
Stopwatch stopWatch = new();
try
{
    stopWatch.Start();
    await fileGenerator.GenerateFile(filePath, FileSizeArgumentParser.ParseSizeToBytes(fileSizeString));
    Console.WriteLine($"Time: {stopWatch.Elapsed}");
    stopWatch.Stop();
}
catch (Exception e)
{
    Console.WriteLine($"Error: {e.Message}");
}