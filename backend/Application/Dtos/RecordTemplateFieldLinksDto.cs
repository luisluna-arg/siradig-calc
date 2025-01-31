
namespace SiradigCalc.Application.Dtos;

public class RecordTemplateFieldLinksDto
{
    public Guid Id { get; set; } = default!;
    public RecordTemplateFieldDto RightField { get; set; } = default!;
    public ICollection<RecordTemplateFieldDto> LeftFields { get; set; } = [];
}