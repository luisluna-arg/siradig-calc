using SiradigCalc.Application.Dtos.Import;

namespace SiradigCalc.Application.Helpers.Pdf;

/// <summary>
/// Turns the raw textual rows of a receipt into structured sections of monetary
/// line items, skipping totals/subtotals. Pure logic with no PDF dependency.
/// </summary>
public interface IReceiptDocumentParser
{
    IReadOnlyList<ImportedSectionDto> Parse(IEnumerable<string> lines);
}

public class ReceiptDocumentParser : IReceiptDocumentParser
{
    private const string DefaultSectionName = "General";

    private static readonly string[] TotalKeywords = ["TOTAL", "NETO", "SUBTOTAL"];

    private enum LineKind
    {
        Skip,
        Header,
        Entry
    }

    public IReadOnlyList<ImportedSectionDto> Parse(IEnumerable<string> lines)
    {
        var sections = new List<ImportedSectionDto>();
        ImportedSectionDto? current = null;

        foreach (var raw in lines)
        {
            var line = raw.Trim();
            if (line.Length == 0)
            {
                continue;
            }

            switch (Classify(line, out var label, out var value))
            {
                case LineKind.Entry:
                    current ??= AppendSection(sections, DefaultSectionName);

                    if (IsTotalRow(label, value, current))
                    {
                        continue;
                    }

                    current.Entries.Add(new ImportedEntryDto { Label = label, Value = value });
                    break;

                case LineKind.Header:
                    current = AppendSection(sections, line);
                    break;

                case LineKind.Skip:
                default:
                    break;
            }
        }

        // Drop header-only sections that never collected a monetary line item.
        return sections.Where(s => s.Entries.Count > 0).ToList();
    }

    private static ImportedSectionDto AppendSection(List<ImportedSectionDto> sections, string name)
    {
        var section = new ImportedSectionDto { Name = name };
        sections.Add(section);
        return section;
    }

    private static LineKind Classify(string line, out string label, out decimal value)
    {
        label = string.Empty;
        value = 0m;

        var tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length == 0)
        {
            return LineKind.Skip;
        }

        if (!ReceiptAmountParser.TryParse(tokens[^1], out value))
        {
            // No numeric value on the right: this is a section boundary.
            return LineKind.Header;
        }

        label = string.Join(' ', tokens[..^1]).Trim();

        // A lone amount with no label is noise, not a line item.
        return label.Length == 0 ? LineKind.Skip : LineKind.Entry;
    }

    private static bool IsTotalRow(string label, decimal value, ImportedSectionDto section)
    {
        var upper = label.ToUpperInvariant();
        if (TotalKeywords.Any(upper.Contains))
        {
            return true;
        }

        if (section.Entries.Count > 0)
        {
            var sum = section.Entries.Sum(e => e.Value);
            if (sum != 0m && sum == value)
            {
                return true;
            }
        }

        return false;
    }
}
