namespace SiradigCalc.Application.Dtos.Conversion.Base;

public abstract class RecordConversionDto : IRecordConversionDto
{
    public required Guid RecordTemplateLinkId { get; set; }
    public required RecordTemplateLinkBasicDto RecordTemplateLink { get; set; }
    public required Guid SourceId { get; set; }
    public required RecordDto Source { get; set; }
    public required Guid TargetId { get; set; }
    public required RecordDto Target { get; set; }
}
