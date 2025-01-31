using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Helpers.Reducers;

public class NumbersReducerStrategy(IDecimalParser decimalParser) : IValuesReducerStrategy<decimal>
{
    private readonly IDecimalParser _decimalParser = decimalParser;

    public decimal Reduce(ICollection<RecordTemplateField> recordTemplateFields, ICollection<RecordValue> values)
        => Reduce(values.Where(v => recordTemplateFields.Any(r => r.Id == v.FieldId)).Select(v => v.Value).ToArray());

    public decimal Reduce(ICollection<string> values)
        => values.Sum(_decimalParser.Parse);

    object IValuesReducerStrategy.Reduce(ICollection<RecordTemplateField> recordTemplateFields, ICollection<RecordValue> values)
        => Reduce(recordTemplateFields, values);

    object IValuesReducerStrategy.Reduce(ICollection<string> strings)
        => Reduce(strings);
}