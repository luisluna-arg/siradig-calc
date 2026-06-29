using SiradigCalc.Application.Helpers.Pdf;

namespace SiradigCalc.Tests.Helpers.Pdf;

// All data in these tests is entirely fictional, per the issue's privacy constraint.
public class ReceiptDocumentParserTests
{
    private readonly ReceiptDocumentParser _parser = new();

    [Fact]
    public void Parse_GroupsEntriesUnderTheirSectionHeaders()
    {
        var lines = new[]
        {
            "Section A",
            "Line item 1 1000.00",
            "Line item 2 250.50",
            "Section B",
            "Line item 3 300.00"
        };

        var sections = _parser.Parse(lines);

        Assert.Equal(2, sections.Count);

        Assert.Equal("Section A", sections[0].Name);
        Assert.Collection(sections[0].Entries,
            e => Assert.Equal(("Line item 1", 1000.00m), (e.Label, e.Value)),
            e => Assert.Equal(("Line item 2", 250.50m), (e.Label, e.Value)));

        Assert.Equal("Section B", sections[1].Name);
        var single = Assert.Single(sections[1].Entries);
        Assert.Equal(("Line item 3", 300.00m), (single.Label, single.Value));
    }

    [Fact]
    public void Parse_ExcludesTotalsByKeyword()
    {
        var lines = new[]
        {
            "Section A",
            "Line item 1 100.00",
            "Line item 2 200.00",
            "SUBTOTAL 300.00",
            "TOTAL NETO 300.00"
        };

        var sections = _parser.Parse(lines);

        var section = Assert.Single(sections);
        Assert.Equal(2, section.Entries.Count);
        Assert.DoesNotContain(section.Entries, e => e.Label.Contains("TOTAL", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Parse_ExcludesRowWhoseValueEqualsSectionSum()
    {
        var lines = new[]
        {
            "Section A",
            "Line item 1 120.00",
            "Line item 2 80.00",
            "Grand sum 200.00" // equals 120 + 80, so it is a computed total
        };

        var sections = _parser.Parse(lines);

        var section = Assert.Single(sections);
        Assert.Equal(2, section.Entries.Count);
        Assert.DoesNotContain(section.Entries, e => e.Value == 200.00m);
    }

    [Fact]
    public void Parse_PlacesOrphanEntriesUnderADefaultSection()
    {
        var lines = new[]
        {
            "Line item 1 10.00"
        };

        var sections = _parser.Parse(lines);

        var section = Assert.Single(sections);
        Assert.Equal("General", section.Name);
        Assert.Single(section.Entries);
    }

    [Fact]
    public void Parse_DropsHeaderOnlySections()
    {
        var lines = new[]
        {
            "A header with no values",
            "Section A",
            "Line item 1 5.00"
        };

        var sections = _parser.Parse(lines);

        var section = Assert.Single(sections);
        Assert.Equal("Section A", section.Name);
    }
}
