namespace SiradigCalc.Application.Dtos.Conversion;

public interface IRecordConversionDto
{
    public Guid RecordFromId { get; set; }
    public Guid RecordToId { get; set; }
}