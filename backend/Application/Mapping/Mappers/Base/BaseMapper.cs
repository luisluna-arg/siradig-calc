using SiradigCalc.Application.Mappers;

namespace SiradigCalc.Application.Mapping.Mappers.Base;

public abstract class BaseMapper<TSource, TTarget> : IDtoMapper<TSource, TTarget>
{
    private readonly IDtoMappingService _dtoMapperManager;

    protected BaseMapper(IDtoMappingService dtoMapperManager)
    {
        _dtoMapperManager = dtoMapperManager;
    }

    // Expose DtoMapperManager to derived classes
    protected IDtoMappingService DtoMapperManager => _dtoMapperManager;

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

    public abstract TTarget Map(TSource source);
}