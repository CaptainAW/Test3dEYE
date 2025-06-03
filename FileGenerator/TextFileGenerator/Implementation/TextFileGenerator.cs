using System.Text;

namespace FileGenerator;

public class TextFileGenerator : ITextFileGenerator
{
    private static readonly Random Rand = new();
    
    public async Task GenerateFile(string filePath, long fileSize)
    {
        long written = 0;
        long lineCount = 0;

        var stringBuilder = new StringBuilder(Constants.MaxPhraseLength);

        await using StreamWriter writer = new StreamWriter(filePath);
        while (written < fileSize)
        {
            int number = Rand.Next(1, Constants.MaxIndexValue);
            string str = Constants.Phrases[Rand.Next(Constants.Phrases.Length)];

            stringBuilder.Clear();
            stringBuilder.Append(number);
            stringBuilder.Append('.');
            stringBuilder.Append(str);

            string line = stringBuilder.ToString();
            await writer.WriteLineAsync(line);
            written += Encoding.UTF8.GetByteCount(line + Environment.NewLine);
            lineCount++;
        }

        Console.WriteLine($"File {filePath} created with {lineCount} lines.");
    }
}