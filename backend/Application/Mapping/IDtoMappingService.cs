namespace SiradigCalc.Application.Mapping;

public interface IDtoMappingService
{
    TResult Map<TResult>(object source);
    ICollection<TResult> Map<TResult>(IEnumerable<object> source);
}