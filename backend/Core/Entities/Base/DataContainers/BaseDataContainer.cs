namespace SiradigCalc.Core.Entities.Base.DataContainers;

public abstract class BaseDataContainer<TSection, TField> : Entity
    where TSection : BaseDataContainerSection<TField>
    where TField : BaseDataContainerField
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<TSection> Sections { get; set; } = new List<TSection>();
}
