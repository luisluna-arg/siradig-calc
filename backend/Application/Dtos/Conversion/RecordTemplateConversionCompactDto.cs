using SiradigCalc.Application.Dtos.Conversion.Base;

namespace SiradigCalc.Application.Dtos.Conversion;

public class RecordTemplateConversionCompactDto : RecordConversionBaseDto
{
    public required Guid Id { get; set; }
}
