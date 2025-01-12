namespace SiradigCalc.Application.Dtos.Conversion.Base;

public interface IRecordConversionDto
{
    public Guid SourceId { get; set; }
    public Guid TargetId { get; set; }
}