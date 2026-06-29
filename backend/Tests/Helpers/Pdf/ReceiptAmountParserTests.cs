using SiradigCalc.Application.Helpers.Pdf;

namespace SiradigCalc.Tests.Helpers.Pdf;

public class ReceiptAmountParserTests
{
    [Theory]
    [InlineData("1000.00", 1000.00)]
    [InlineData("250.50", 250.50)]
    [InlineData("300", 300)]
    [InlineData("1.234,56", 1234.56)]   // Argentine format
    [InlineData("1,234.56", 1234.56)]   // invariant with thousands
    [InlineData("$ 99.99", 99.99)]
    [InlineData("(150.00)", -150.00)]   // parentheses denote a negative
    public void TryParse_ParsesSupportedFormats(string token, decimal expected)
    {
        var parsed = ReceiptAmountParser.TryParse(token, out var value);

        Assert.True(parsed);
        Assert.Equal(expected, value);
    }

    [Theory]
    [InlineData("Section")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("N/A")]
    public void TryParse_RejectsNonNumericTokens(string token)
    {
        var parsed = ReceiptAmountParser.TryParse(token, out var value);

        Assert.False(parsed);
        Assert.Equal(0m, value);
    }
}
