namespace SiradigCalc.Application.Dtos;

public class RecordTemplateLinkDto
{
    public RecordTemplateDto FormTemplate { get; set; } = default!;
    public RecordTemplateDto ReceiptTemplate { get; set; } = default!;
    public ICollection<RecordFieldLinkDto> RecordFieldLinks { get; set; } = [];
}