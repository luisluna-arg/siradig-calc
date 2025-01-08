using SiradigCalc.Core.Entities.Enums;

namespace SiradigCalc.Application.Dtos.Conversion;

public class FieldValueDto
{
    public required string Label { get; set; }
    public Guid FieldId { get; set; }
    public FieldType FieldType { get; set; }
    public bool IsRequired { get; set; }
    public required object Value { get; set; }
}