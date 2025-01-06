namespace SiradigCalc.Application.Mappers;

public class DtoMapperManager : IDtoMapperManager
{
    private readonly IEnumerable<IDtoMapper> _strategies;

    public DtoMapperManager(IEnumerable<IDtoMapper> strategies)
    {
        _strategies = strategies;
    }

    public TResult Map<TResult>(object source)
    {
        var mapper = _strategies.FirstOrDefault(s => s.IsMappingEnabled(source.GetType()));

        if (mapper != null)
        {
            return (TResult)mapper.Map(source);
        }

        throw new InvalidOperationException($"No mapper found from '{source.GetType().Name}' to '{typeof(TResult).Name}'.");
    }

    public ICollection<TResult> Map<TResult>(IEnumerable<object> source)
        => source.Select(s => Map<TResult>(s)).ToList();
}
