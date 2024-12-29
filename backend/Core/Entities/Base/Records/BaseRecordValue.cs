namespace SiradigCalc.Core.Entities.Base.Records;

public abstract class BaseRecordValue<TField>() : Entity()
    where TField : BaseRecordField
{
    public Guid FieldId { get; set; }
    public TField Field { get; set; } = null!;
    public string Value { get; set; } = string.Empty;
}
