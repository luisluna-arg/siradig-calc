namespace SiradigCalc.Core.Entities.Base.Records;

public abstract class BaseRecordInstance<TRecord, TRecordTemplateId, TRecordTemplate, TRecordSection, TField, TValue>() : Record()
    where TRecord : BaseRecordInstance<TRecord, TRecordTemplateId, TRecordTemplate, TRecordSection, TField, TValue>
    where TRecordTemplate : BaseRecordTemplate<TRecordSection, TField>
    where TRecordSection : BaseRecordSection<TField>
    where TField : BaseRecordField
    where TValue : BaseRecordValue<TRecord, TRecordTemplateId, TRecordTemplate, TRecordSection, TField, TValue>
{
    public TRecordTemplateId RecordTemplateId { get; set; } = default!;
    public TRecordTemplate RecordTemplate { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public ICollection<TValue> Values { get; set; } = [];
    public ICollection<ReceiptToFormConversion> ReceiptToFormConversions { get; set; } = [];
}
