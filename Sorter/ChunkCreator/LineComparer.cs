namespace Sorter;

public class LineComparer : IComparer<Line>
{
    public int Compare(Line a, Line b)
    {
        int cmp = string.Compare(a.Phrase, b.Phrase, StringComparison.Ordinal);
        return cmp != 0 ? cmp : a.Number.CompareTo(b.Number);
    }
}