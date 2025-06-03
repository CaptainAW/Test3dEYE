namespace Sorter;

public static class LineParser
{
    public static Line Parse(string stringLine)
    {
        var dotIndex = stringLine.IndexOf('.');
        if (dotIndex < 0 || !int.TryParse(stringLine[..dotIndex], out int number))
            throw new FormatException($"Invalid line format: {stringLine}");

        var strPart = stringLine[(dotIndex + 1)..];
        return new Line { Number = number, Phrase = strPart, FullString = stringLine };
    }
}