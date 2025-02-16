namespace SiradigCalc.Application.Mappers;

public interface IDtoMapper<TSource, TTarget> : IDtoMapper
{
    TTarget Map(TSource source);
    IEnumerable<TTarget> Map(IEnumerable<TSource> source);
}

public interface IDtoMapper
{
    bool IsMappingEnabled(Type sourceType, Type destinationType);
    object Map(object source);
    IEnumerable<object> Map(IEnumerable<object> source);
}