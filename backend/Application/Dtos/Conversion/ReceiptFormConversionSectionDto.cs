namespace SiradigCalc.Application.Dtos.Conversion;

public class ReceiptFormConversionSectionDto
{
    public required ICollection<FieldValueDto> Fields { get; set; }
}