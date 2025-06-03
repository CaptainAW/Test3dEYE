using Sorter;

namespace TestProject.Sorter;

public class LineParserTest
{
    [Test]
    public void ParseLine()
    {
        Line line = LineParser.Parse("123123.Some Phrase");
        Assert.Multiple(() =>
        {
            Assert.That(line.Number, Is.EqualTo(123123));
            Assert.That(line.Phrase, Is.EqualTo("Some Phrase"));
        });
    }
}