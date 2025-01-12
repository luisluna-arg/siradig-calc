namespace SiradigCalc.Core.Entities.Base.Records;

public abstract class RecordConversionBase<TSource, TTarget> : Entity
    where TSource: Record 
    where TTarget: Record
{
    public Guid RecordTemplateLinkId { get; set; }
    public RecordTemplateLink RecordTemplateLink { get; set; } = default!;
    public Guid SourceId { get; set; }
    public TSource Source { get; set; } = default!;
    public Guid TargetId { get; set; }
    public TTarget Target { get; set; } = default!;
}