namespace SiradigCalc.Application.Dtos;

public class RecordTemplateLinkDto
{
    public Guid Id { get; set; } = default!;
    public RecordTemplateDto FormTemplate { get; set; } = default!;
    public RecordTemplateDto ReceiptTemplate { get; set; } = default!;
    public ICollection<FormFieldLinksDto> RecordFieldLinks { get; set; } = [];
}