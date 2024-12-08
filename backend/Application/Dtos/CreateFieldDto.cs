namespace SiradigCalc.Application.Dtos;

public class CreateFieldDto
{
    public string Label { get; set; } = string.Empty;
    public int FieldType { get; set; }
    public bool IsRequired { get; set; }
    public string Placeholder { get; set; } = string.Empty;
}
