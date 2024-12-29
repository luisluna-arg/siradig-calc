namespace SiradigCalc.Core.Entities.Base.Records;

public abstract class BaseRecordInstance<TRecord, TRecordId, TRecordSection, TFieldValue, TField>() : Entity()
    where TRecord : BaseRecordTemplate<TRecordSection, TField>
    where TRecordSection : BaseRecordSection<TField>
    where TField : BaseRecordField
    where TFieldValue : BaseRecordValue<TField>
{
    public TRecordId RecordId { get; set; } = default!;
    public TRecord Record { get; set; } = null!;
    public ICollection<TFieldValue> Values { get; set; } = new List<TFieldValue>();
}
