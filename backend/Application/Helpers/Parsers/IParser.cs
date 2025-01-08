namespace SiradigCalc.Application.Helpers.Reducers;

public interface IParserStrategy
{
    object Parse(string input);
}

public interface IParserStrategy<TValue> : IParserStrategy
{
    new TValue  Parse(string input);

    object IParserStrategy.Parse(string input) => Parse(input)!;
}
