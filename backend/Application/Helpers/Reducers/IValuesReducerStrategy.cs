using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Helpers.Reducers;

/* TODO It shouldn't be needed to return object */
public interface IValuesReducerStrategy
{
    object Reduce(ICollection<RecordTemplateField> recordTemplateFields, ICollection<RecordValue> values);
    object Reduce(ICollection<string> values);
}

public interface IValuesReducerStrategy<TValue> : IValuesReducerStrategy
{
    new TValue Reduce(ICollection<RecordTemplateField> recordTemplateFields, ICollection<RecordValue> values);

    new TValue Reduce(ICollection<string> values);
}
