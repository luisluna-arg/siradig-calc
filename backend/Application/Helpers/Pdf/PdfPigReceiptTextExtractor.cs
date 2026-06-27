using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace SiradigCalc.Application.Helpers.Pdf;

/// <summary>
/// Extracts the textual rows of a payroll receipt PDF, preserving the
/// two-column (label on the left, amount on the right) layout on each line.
/// </summary>
public interface IReceiptPdfTextExtractor
{
    IReadOnlyList<string> ExtractLines(Stream pdfStream);
}

/// <summary>
/// <see cref="IReceiptPdfTextExtractor"/> backed by PdfPig. Groups the words of
/// every page into visual rows by their vertical position and orders each row
/// left-to-right, so a "label ... amount" pair ends up on a single line.
/// </summary>
public class PdfPigReceiptTextExtractor : IReceiptPdfTextExtractor
{
    public IReadOnlyList<string> ExtractLines(Stream pdfStream)
    {
        var lines = new List<string>();

        using var document = PdfDocument.Open(pdfStream);

        foreach (var page in document.GetPages())
        {
            lines.AddRange(ExtractPageLines(page));
        }

        return lines;
    }

    private static IEnumerable<string> ExtractPageLines(Page page)
    {
        var words = page.GetWords()
            .Where(w => !string.IsNullOrWhiteSpace(w.Text))
            .ToList();

        if (words.Count == 0)
        {
            yield break;
        }

        // Tolerance used to decide whether two words sit on the same visual row.
        var tolerance = words.Average(w => w.BoundingBox.Height) * 0.5;

        var rows = new List<List<Word>>();

        foreach (var word in words.OrderByDescending(w => w.BoundingBox.Bottom))
        {
            var currentRow = rows.LastOrDefault();

            if (currentRow != null &&
                Math.Abs(currentRow[0].BoundingBox.Bottom - word.BoundingBox.Bottom) <= tolerance)
            {
                currentRow.Add(word);
            }
            else
            {
                rows.Add([word]);
            }
        }

        foreach (var row in rows)
        {
            var text = string.Join(' ', row
                .OrderBy(w => w.BoundingBox.Left)
                .Select(w => w.Text));

            if (!string.IsNullOrWhiteSpace(text))
            {
                yield return text;
            }
        }
    }
}
