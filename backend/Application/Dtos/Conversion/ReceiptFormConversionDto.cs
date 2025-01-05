namespace SiradigCalc.Application.Dtos.Conversion;

public class ReceiptFormConversionDto : IRecordConversionDto
{
    public Guid RecordFromId { get; set; }
    public Guid RecordToId { get; set; }
    public required string FormName { get; set; }
    public required string ReceiptName { get; set; }
    public required ICollection<FieldValueDto> Values { get; set; }
}
