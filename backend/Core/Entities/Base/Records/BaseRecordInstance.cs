namespace SiradigCalc.Core.Entities.Base.Records;

public abstract class BaseRecordInstance<TRecord, TRecordId, TRecordSection, TField, TValue>() : Entity()
    where TRecord : BaseRecordTemplate<TRecordSection, TField>
    where TRecordSection : BaseRecordSection<TField>
    where TField : BaseRecordField
    where TValue : BaseRecordValue<TField>
{
    public TRecordId RecordId { get; set; } = default!;
    public TRecord Record { get; set; } = null!;
    public ICollection<TValue> Values { get; set; } = new List<TValue>();
}
