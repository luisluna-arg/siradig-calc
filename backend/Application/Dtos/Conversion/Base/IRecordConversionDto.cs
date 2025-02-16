namespace SiradigCalc.Application.Dtos.Conversion.Base;

public interface IRecordConversionBaseDto
{
    public Guid SourceId { get; set; }
    public Guid TargetId { get; set; }
}