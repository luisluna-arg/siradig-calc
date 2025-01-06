namespace SiradigCalc.Application.Mappers.Base;

public abstract class BaseMapper<TSource, TTarget>(IDtoMapperManager dtoMapperManager)
    : IDtoMapper<TSource, TTarget>
{
    protected IDtoMapperManager DtoMapperManager { get; } = dtoMapperManager;

    public bool IsMappingEnabled(Type sourceType)
    {
        return sourceType == typeof(TSource);
    }

    public abstract TTarget Map(TSource source);

    public object Map(object source) => Map((TSource)source)!;
}
