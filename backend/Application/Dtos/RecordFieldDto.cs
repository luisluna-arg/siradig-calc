namespace SiradigCalc.Application.Dtos;

public class RecordFieldDto
{
    public string Label { get; set; } = string.Empty;
    public FieldTypeDto FieldType { get; set; } = default!;
    public bool IsRequired { get; set; }
}