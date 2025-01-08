using System.Globalization;

namespace SiradigCalc.Application.Helpers.Reducers;

public class DecimalParserStrategy : IDecimalParser
{
    public decimal Parse(string input)
    {
        if (int.TryParse(input, CultureInfo.InvariantCulture, out int intResult))
        {
            return intResult;
        }
        else if (decimal.TryParse(input, CultureInfo.InvariantCulture, out decimal decimalResult))
        {
            return decimalResult;
        }
        else
        {
            throw new FormatException($"Input '{input}' is not a valid number.");
        }
    }
}

public interface IDecimalParser : IParserStrategy<decimal>
{
}