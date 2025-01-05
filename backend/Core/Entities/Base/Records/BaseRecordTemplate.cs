namespace SiradigCalc.Core.Entities.Base.Records;

public abstract class BaseRecordTemplate<TSection, TField> : RecordTemplate
    where TSection : BaseRecordSection<TField>
    where TField : BaseRecordField
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<TSection> Sections { get; set; } = new List<TSection>();
}
