namespace SiradigCalc.Core.Entities.Base.Records;

public abstract class BaseRecordSection<TField> : Entity
    where TField : BaseRecordField
{
    public string Name { get; set; } = string.Empty;
    public ICollection<TField> Fields { get; set; } = new List<TField>();
}
