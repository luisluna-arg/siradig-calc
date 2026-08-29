using SiradigCalc.Core.Entities.Enums;

namespace SiradigCalc.Application.Dtos.Conversion;

public class FieldCompositionDto
{
    public required Guid FieldId { get; set; }
    public required string Label { get; set; }
    public FieldType FieldType { get; set; }
    public bool IsRequired { get; set; }
    public required object Value { get; set; }
    public required FieldValueDto[] SourceFields { get; set; }
}
