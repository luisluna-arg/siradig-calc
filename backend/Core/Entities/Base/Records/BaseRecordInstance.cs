namespace SiradigCalc.Core.Entities.Base.Records;

public abstract class BaseRecordInstance<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>() : Entity()
    where TRecord : BaseRecordInstance<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>
    where TRecordTemplate : BaseRecordTemplate<TRecordSection, TField>
    where TRecordSection : BaseRecordSection<TField>
    where TField : BaseRecordField
    where TValue : BaseRecordValue<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>
{
    public TRecordId RecordId { get; set; } = default!;
    public TRecordTemplate Record { get; set; } = null!;
    public ICollection<TValue> Values { get; set; } = new List<TValue>();
}
