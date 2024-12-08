namespace SiradigCalc.Core.Entities.Base.DataContainers;

public abstract class BaseDataContainerValue<TField>() : Entity()
    where TField : BaseDataContainerField
{
    public Guid FieldId { get; set; }
    public TField Field { get; set; } = null!;
    public string Value { get; set; } = string.Empty;
}
