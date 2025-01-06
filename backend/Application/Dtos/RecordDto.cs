
namespace SiradigCalc.Application.Dtos;

public class RecordDto
{
    public Guid RecordTemplateId { get; set; } = default!;
    public ICollection<FieldValueDto> Values { get; set; } = [];
}