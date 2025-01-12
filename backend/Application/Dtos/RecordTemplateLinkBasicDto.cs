namespace SiradigCalc.Application.Dtos;

public class RecordTemplateLinkBasicDto
{
    public Guid Id { get; set; } = default!;
    public Guid SourceTemplateId { get; set; } = default!;
    public Guid TargetTemplateId { get; set; } = default!;
}