using SiradigCalc.Core.Entities.Enums;

namespace SiradigCalc.Application.Dtos;

public class CreateFieldDto
{
    public string Label { get; set; } = string.Empty;
    public FieldType FieldType { get; set; }
    public bool IsRequired { get; set; }
}
