using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Application.Helpers.Reducers;

public class StringsReducerStrategy : IValuesReducerStrategy<string>
{
    public string Reduce(ICollection<ReceiptField> receiptFields, ICollection<ReceiptValue> values)
        => string.Join(",", values.Where(v => receiptFields.Any(r => r.Id == v.FieldId)).Select(v => v.Value).ToArray());
}