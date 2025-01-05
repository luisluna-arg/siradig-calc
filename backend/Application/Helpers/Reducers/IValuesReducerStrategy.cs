using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Application.Helpers.Reducers;

public interface IValuesReducerStrategy
{
    object Reduce(ICollection<ReceiptField> receiptFields, ICollection<ReceiptValue> values);
}

public interface IValuesReducerStrategy<TValue> : IValuesReducerStrategy
{
    new TValue Reduce(ICollection<ReceiptField> receiptFields, ICollection<ReceiptValue> values);

    object IValuesReducerStrategy.Reduce(ICollection<ReceiptField> receiptFields, ICollection<ReceiptValue> values)
    {
        return Reduce(receiptFields, values)!;
    }
}
