namespace SiradigCalc.Application.Dtos;

public class RecordTemplateLinkDto
{
    public Guid Id { get; set; } = default!;
    public RecordTemplateDto LeftTemplate { get; set; } = default!;
    public RecordTemplateDto RightTemplate { get; set; } = default!;
    public ICollection<RecordTemplateFieldLinksDto> RecordTemplateFieldLinks { get; set; } = [];
}