using SiradigCalc.Core.Entities.Enums;

namespace SiradigCalc.Application.Helpers.Reducers;

public class ValuesReducerStrategyFactory
{
    private readonly IDecimalParser _decimalParser;
    private readonly Dictionary<FieldType, IValuesReducerStrategy> _strategies;

    public ValuesReducerStrategyFactory(IDecimalParser decimalParser)
    {
        _decimalParser = decimalParser;
        _strategies = new Dictionary<FieldType, IValuesReducerStrategy>();
    }

    public IValuesReducerStrategy GetStrategy(FieldType fieldType)
    {
        if (_strategies.ContainsKey(fieldType))
        {
            return _strategies[fieldType];
        }

        switch (fieldType)
        {
            case FieldType.Number:
                _strategies[fieldType] = new NumbersReducerStrategy(_decimalParser);
                break;
            default:
                _strategies[fieldType] = new StringsReducerStrategy();
                break;
        }

        return _strategies[fieldType];
    }
}
