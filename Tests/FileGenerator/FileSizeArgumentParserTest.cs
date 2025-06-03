using FileGenerator;

namespace TestProject;

public class FileSizeArgumentParserTest
{
    [Test]
    public void ParseBytes()
    {
        int amount = 21231;
        Assert.That(FileSizeArgumentParser.ParseSizeToBytes($"{amount}b"), Is.EqualTo(amount));
    }

    [Test]
    public void ParseKb()
    {
        int amount = 771;
        Assert.That(FileSizeArgumentParser.ParseSizeToBytes($"{amount}Kb"), Is.EqualTo(amount * 1024));
    }

    [Test]
    public void ParseMb()
    {
        int amount = 83;
        Assert.That(FileSizeArgumentParser.ParseSizeToBytes($"{amount}Mb"), Is.EqualTo(amount * 1024 * 1024));
    }

    [Test]
    public void ParseGb()
    {
        long amount = 127;
        Assert.That(FileSizeArgumentParser.ParseSizeToBytes($"{amount}Gb"), Is.EqualTo(amount * 1024 * 1024 * 1024));
    }
}