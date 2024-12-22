namespace SiradigCalc.Core.Entities.Base.DataContainers;

public abstract class BaseDataContainerInstance<TDataContainer, TDataContainerId, TDataContainerSection, TFieldValue, TField>() : Entity()
    where TDataContainer : BaseDataContainer<TDataContainerSection, TField>
    where TDataContainerSection : BaseDataContainerSection<TField>
    where TField : BaseDataContainerField
    where TFieldValue : BaseDataContainerValue<TField>
{
    public TDataContainerId DataContainerId { get; set; } = default!;
    public TDataContainer DataContainer { get; set; } = null!;
    public ICollection<TFieldValue> Values { get; set; } = new List<TFieldValue>();
}
