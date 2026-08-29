using SiradigCalc.Application.Dtos.Conversion.Base;

namespace SiradigCalc.Application.Dtos.Conversion;

public class RecordTemplateConversionDto : RecordConversionBaseDto
{
    public required Guid Id { get; set; }
    public required decimal Haberes { get; set; }
    public required decimal Retenciones { get; set; }
    public required decimal Neto { get; set; }
    public required FieldCompositionDto[] Values { get; set; }
}
