namespace SiradigCalc.Application.Dtos;

public class RecordTemplateSectionDto
{
    public string Name { get; set; } = string.Empty;
    public ICollection<RecordTemplateFieldDto> Fields { get; set; } = new List<RecordTemplateFieldDto>();
}