namespace SiradigCalc.Core.Entities.Base.DataContainers;

public abstract class BaseDataContainer<TField> : Entity
    where TField : BaseDataContainerField
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<TField> Fields { get; set; } = new List<TField>();
}
