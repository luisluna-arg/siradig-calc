namespace SiradigCalc.Application.Dtos;

public class RecordTemplateFieldLinkDto
{
    public Guid TemplateLinkId { get; set; }
    public RecordTemplateFieldDto LeftTemplateField { get; set; } = default!;
    public RecordTemplateFieldDto RightTemplateField { get; set; } = default!;
}