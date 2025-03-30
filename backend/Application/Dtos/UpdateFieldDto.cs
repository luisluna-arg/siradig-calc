using SiradigCalc.Core.Entities.Enums;

namespace SiradigCalc.Application.Dtos;

public class UpdateFieldDto
{
    public Guid? Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public FieldType FieldType { get; set; }
    public bool IsRequired { get; set; }
}
