namespace SiradigCalc.Application.Dtos;

public class RecordValueDto
{
    public Guid FieldId { get; set; }
    public string Label { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}