namespace SiradigCalc.Core.Entities.Base.DataContainers;

public abstract class BaseDataContainerSection<TField> : Entity
    where TField : BaseDataContainerField
{
    public string Name { get; set; } = string.Empty;
    public ICollection<TField> Fields { get; set; } = new List<TField>();
}
