namespace SiradigCalc.Application.Dtos.Conversion.Base;

public interface IRecordConversionBaseDto
{
    public RecordDto Source { get; set; }
    public RecordDto Target { get; set; }
}

public abstract class RecordConversionBaseDto : IRecordConversionBaseDto
{
    public required Guid RecordTemplateLinkId { get; set; }
    public required RecordTemplateLinkBasicDto RecordTemplateLink { get; set; }
    public required RecordDto Source { get; set; }
    public required RecordDto Target { get; set; }
}
