using SiradigCalc.Core.Entities.Enums;

namespace SiradigCalc.Core.Entities.Base.DataContainers;

public abstract class BaseDataContainerField : Entity<Guid>
{
    public string Label { get; set; } = string.Empty;
    public FieldType FieldType { get; set; } = default;
    public bool IsRequired { get; set; }
}
