using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Application.Helpers.Reducers;

public class StringsReducerStrategy : IValuesReducerStrategy<string>
{
    public string Reduce(ICollection<ReceiptField> receiptFields, ICollection<ReceiptValue> values)
        => Reduce(values.Where(v => receiptFields.Any(r => r.Id == v.FieldId)).Select(v => v.Value).ToArray());

    public string Reduce(ICollection<string> values)
        => string.Join(",", values.Select(v => v));

    object IValuesReducerStrategy.Reduce(ICollection<ReceiptField> receiptFields, ICollection<ReceiptValue> values)
        => Reduce(receiptFields, values);

    object IValuesReducerStrategy.Reduce(ICollection<string> values)
        => Reduce(values);
}