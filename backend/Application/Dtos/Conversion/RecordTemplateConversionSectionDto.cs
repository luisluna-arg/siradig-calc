namespace SiradigCalc.Application.Dtos.Conversion;

public class RecordTemplateConversionSectionDto
{
    public required ICollection<FieldValueDto> Fields { get; set; }
}