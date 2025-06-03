using Sorter;

namespace TestProject.Sorter;

public class LineComparerTest
{
    private readonly LineComparer _comparer = new();

    [Test]
    public void CompareEqual()
    {
        Line a = LineParser.Parse("123123.Alpha And Omega");
        Line b = LineParser.Parse("123123.Alpha And Omega");
        Assert.That(_comparer.Compare(a, b), Is.Zero);
    }

    [Test]
    public void A_More_Than_B_By_String()
    {
        Line a = LineParser.Parse("7712.Queen an King");
        Line b = LineParser.Parse("1113334.And there is nobody");
        Assert.That(_comparer.Compare(a, b), Is.Positive);
    }

    [Test]
    public void A_More_Than_B_By_Number()
    {
        Line a = LineParser.Parse("1113334.Queen an King");
        Line b = LineParser.Parse("1234.Queen an King");
        Assert.That(_comparer.Compare(a, b), Is.Positive);
    }


    [Test]
    public void A_Less_Than_B_By_String()
    {
        Line a = LineParser.Parse("123123.Alpha And Omega");
        Line b = LineParser.Parse("32312.Beta And Omega");
        Assert.That(_comparer.Compare(a, b), Is.Negative);
    }

    [Test]
    public void A_Less_Than_B_By_Number()
    {
        Line a = LineParser.Parse("23123.Alpha And Omega");
        Line b = LineParser.Parse("123123.Alpha And Omega");
        Assert.That(_comparer.Compare(a, b), Is.Negative);
    }
}