namespace SiradigCalc.Application.Mappers;

public interface IDtoMapperManager
{
    TResult Map<TResult>(object source);
    ICollection<TResult> Map<TResult>(IEnumerable<object> source);
}