namespace SiradigCalc.Application.Dtos;

public class RecordFieldLinkDto
{
    public Guid TemplateLinkId { get; set; }
    public RecordFieldDto FormField { get; set; } = default!;
    public RecordFieldDto ReceiptField { get; set; } = default!;
}