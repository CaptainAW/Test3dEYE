using System.Globalization;
using System.Text.RegularExpressions;

namespace FileGenerator;

public static class FileSizeArgumentParser
{
    public static long ParseSizeToBytes(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input is null or empty");

        var match = Regex.Match(input.Trim(), @"^(?<value>\d+)(?<unit>KB|MB|GB|B)$", RegexOptions.IgnoreCase);
        if (!match.Success)
            throw new FormatException("Invalid size format. Use nB, nKB, nMB, or nGB");

        long value = int.Parse(match.Groups["value"].Value, CultureInfo.InvariantCulture);
        string unit = match.Groups["unit"].Value.ToUpperInvariant();

        return unit switch
        {
            "B" => value,
            "KB" => checked(value * 1024),
            "MB" => checked(value * 1024 * 1024),
            "GB" => checked(value * 1024 * 1024 * 1024),
            _ => throw new NotSupportedException("Unsupported unit")
        };
    }
}