namespace SiradigCalc.Application.Dtos.Conversion.Base;

public interface IRecordConversionBaseDto
{
    public RecordDto Source { get; set; }
    public RecordDto Target { get; set; }
}