using SiradigCalc.Application.Mappers;

namespace SiradigCalc.Application.Mapping.Mappers.Base;

public abstract class BaseMapper<TSource, TTarget> : IDtoMapper<TSource, TTarget>
{
    protected IDtoMappingService DtoMappingService { get; private set; }

    protected BaseMapper(IDtoMappingService dtoMappingService)
    {
        DtoMappingService = dtoMappingService;
    }

    public bool IsMappingEnabled(Type sourceType, Type destinationType)
    {
        return sourceType == typeof(TSource) && destinationType == typeof(TTarget);
    }

    public object Map(object source)
    {
        if (source is not TSource typedSource)
            throw new InvalidCastException($"Expected type {typeof(TSource).Name}, but got {source.GetType().Name}.");

        return Map(typedSource)!;
    }

    public IEnumerable<object> Map(IEnumerable<object> source)
    {
        if (source is not IEnumerable<TSource> typedSource)
            throw new InvalidCastException($"Expected type {typeof(TSource).Name}, but got {source.GetType().Name}.");

        return Map(typedSource).Cast<object>()!;
    }

    public abstract TTarget Map(TSource source);
    public virtual IEnumerable<TTarget> Map(IEnumerable<TSource> source)
        => source.Select(Map).ToList();
}