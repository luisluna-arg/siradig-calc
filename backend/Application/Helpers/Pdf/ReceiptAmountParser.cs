using System.Globalization;

namespace SiradigCalc.Application.Helpers.Pdf;

/// <summary>
/// Parses the amount token of a receipt row. Handles both invariant
/// (<c>1234.56</c>) and Argentine (<c>1.234,56</c>) decimal formats, optional
/// currency symbols, and negative amounts expressed with a sign or parentheses.
/// </summary>
public static class ReceiptAmountParser
{
    public static bool TryParse(string token, out decimal value)
    {
        value = 0m;

        if (string.IsNullOrWhiteSpace(token))
        {
            return false;
        }

        var sanitized = token.Trim().Replace("$", string.Empty).Replace(" ", string.Empty);

        var negative = false;
        if (sanitized.StartsWith('(') && sanitized.EndsWith(')'))
        {
            negative = true;
            sanitized = sanitized[1..^1];
        }

        if (!sanitized.Any(char.IsDigit))
        {
            return false;
        }

        var hasComma = sanitized.Contains(',');
        var hasDot = sanitized.Contains('.');

        if (hasComma && hasDot)
        {
            // The right-most separator is the decimal separator.
            if (sanitized.LastIndexOf(',') > sanitized.LastIndexOf('.'))
            {
                sanitized = sanitized.Replace(".", string.Empty).Replace(',', '.');
            }
            else
            {
                sanitized = sanitized.Replace(",", string.Empty);
            }
        }
        else if (hasComma)
        {
            // Only a comma is present: treat it as the decimal separator.
            sanitized = sanitized.Replace(',', '.');
        }

        const NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;

        if (!decimal.TryParse(sanitized, styles, CultureInfo.InvariantCulture, out value))
        {
            value = 0m;
            return false;
        }

        if (negative)
        {
            value = -value;
        }

        return true;
    }
}
