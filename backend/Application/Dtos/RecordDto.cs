
namespace SiradigCalc.Application.Dtos;

public class RecordDto
{
    public Guid Id { get; set; } = default!;
    public string? Title { get; set; } = default!;
    public string? Name { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public Guid RecordTemplateId { get; set; } = default!;
    public ICollection<RecordValueDto> Values { get; set; } = [];
}