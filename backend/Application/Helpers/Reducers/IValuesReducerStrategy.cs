using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Application.Helpers.Reducers;

/* TODO It shouldn't be needed to return object */
public interface IValuesReducerStrategy
{
    object Reduce(ICollection<ReceiptField> receiptFields, ICollection<ReceiptValue> values);
    object Reduce(ICollection<string> values);
}

public interface IValuesReducerStrategy<TValue> : IValuesReducerStrategy
{
    new TValue Reduce(ICollection<ReceiptField> receiptFields, ICollection<ReceiptValue> values);

    new TValue Reduce(ICollection<string> values);
}
