using SiradigCalc.Core.Entities.Enums;

namespace SiradigCalc.Application.Helpers.Reducers;

public class ValuesReducerStrategyFactory
{
    private readonly Dictionary<FieldType, IValuesReducerStrategy> _strategies;

    public ValuesReducerStrategyFactory()
    {
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
                _strategies[fieldType] = new NumbersReducerStrategy();
                break;
            default:
                _strategies[fieldType] = new StringsReducerStrategy();
                break;
        }

        return _strategies[fieldType];
    }
}
