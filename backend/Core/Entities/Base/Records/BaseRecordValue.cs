namespace SiradigCalc.Core.Entities.Base.Records;

public abstract class BaseRecordValue<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>() : Entity()
    where TRecordTemplate : BaseRecordTemplate<TRecordSection, TField>
    where TRecordSection : BaseRecordSection<TField>
    where TValue : BaseRecordValue<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>
    where TRecord : BaseRecordInstance<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>
    where TField : BaseRecordField
{
    public TRecordId RecordId { get; set; } = default!;
    public TRecord Record { get; set; } = null!;
    public Guid FieldId { get; set; }
    public TField Field { get; set; } = null!;
    public string Value { get; set; } = string.Empty;
}
