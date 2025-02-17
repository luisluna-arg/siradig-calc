namespace SiradigCalc.Application.Dtos.Conversion.Base;

public abstract class RecordConversionBaseDto : IRecordConversionBaseDto
{
    public required Guid RecordTemplateLinkId { get; set; }
    public required RecordTemplateLinkBasicDto RecordTemplateLink { get; set; }
    public required RecordDto Source { get; set; }
    public required RecordDto Target { get; set; }
}
