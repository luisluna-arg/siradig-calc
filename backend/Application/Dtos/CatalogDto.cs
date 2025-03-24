namespace SiradigCalc.Application.Dtos;

public interface ICatalog { }

public class CatalogDto<T>() : ICatalog
    where T : struct
{
    public T Id { get; set; } = default;
    public string Label { get; set; } = default!;
}

public class CatalogDto() : CatalogDto<Guid>, ICatalog
{
}
