namespace SiradigCalc.Core.Entities.Base.DataContainers;

public abstract class BaseDataContainerInstance<TDataContainer, TDataContainerId, TFieldValue, TField>() : Entity()
    where TDataContainer : BaseDataContainer<TField>
    where TFieldValue : BaseDataContainerValue<TField>
    where TField : BaseDataContainerField
{
    public TDataContainerId DataContainerId { get; set; } = default!;
    public TDataContainer DataContainer { get; set; } = null!;
    public ICollection<TFieldValue> Values { get; set; } = new List<TFieldValue>();
}
