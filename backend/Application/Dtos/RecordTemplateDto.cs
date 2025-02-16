namespace SiradigCalc.Application.Dtos;

public class RecordTemplateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<RecordTemplateSectionDto> Sections { get; set; } = [];
}