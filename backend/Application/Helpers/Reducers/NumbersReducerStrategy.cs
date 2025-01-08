using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Application.Helpers.Reducers;

public class NumbersReducerStrategy(IDecimalParser decimalParser) : IValuesReducerStrategy<decimal>
{
    private readonly IDecimalParser _decimalParser = decimalParser;

    public decimal Reduce(ICollection<ReceiptField> receiptFields, ICollection<ReceiptValue> values)
        => Reduce(values.Where(v => receiptFields.Any(r => r.Id == v.FieldId)).Select(v => v.Value).ToArray());

    public decimal Reduce(ICollection<string> values)
        => values.Sum(_decimalParser.Parse);

    object IValuesReducerStrategy.Reduce(ICollection<ReceiptField> receiptFields, ICollection<ReceiptValue> values)
        => Reduce(receiptFields, values);

    object IValuesReducerStrategy.Reduce(ICollection<string> strings)
        => Reduce(strings);
}