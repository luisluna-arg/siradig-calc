namespace SiradigCalc.Application.Dtos;

public class RecordValueDto
{
    public Guid RecordId { get; set; } = default!;
    public Guid FieldId { get; set; }
    public string Value { get; set; } = string.Empty;
}