namespace FileGenerator;

public interface ITextFileGenerator
{
    public Task GenerateFile(string filePath, long fileSize);
}