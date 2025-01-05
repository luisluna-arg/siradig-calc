using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Application.Helpers.Reducers;

public class NumbersReducerStrategy : IValuesReducerStrategy<decimal>
{
    public decimal Reduce(ICollection<ReceiptField> receiptFields, ICollection<ReceiptValue> values)
        => values.Where(v => receiptFields.Any(r => r.Id == v.FieldId)).Select(v => v.Value).Sum(ParseNumber);

    private static decimal ParseNumber(string input)
    {
        if (int.TryParse(input, out int intResult))
        {
            return intResult;
        }
        else if (decimal.TryParse(input, out decimal decimalResult))
        {
            return decimalResult;
        }
        else
        {
            throw new FormatException("Input is not a valid number.");
        }
    }

    public decimal Reduce(IEnumerable<decimal> values)
    {
        throw new NotImplementedException();
    }
}