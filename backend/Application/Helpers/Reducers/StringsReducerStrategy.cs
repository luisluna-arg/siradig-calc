using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Helpers.Reducers;

public class StringsReducerStrategy : IValuesReducerStrategy<string>
{
    public string Reduce(ICollection<RecordTemplateField> recordTemplateFields, ICollection<RecordValue> values)
        => Reduce(values.Where(v => recordTemplateFields.Any(r => r.Id == v.FieldId)).Select(v => v.Value).ToArray());

    public string Reduce(ICollection<string> values)
        => string.Join(",", values.Select(v => v));

    object IValuesReducerStrategy.Reduce(ICollection<RecordTemplateField> recordTemplateFields, ICollection<RecordValue> values)
        => Reduce(recordTemplateFields, values);

    object IValuesReducerStrategy.Reduce(ICollection<string> values)
        => Reduce(values);
}