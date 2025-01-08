
namespace SiradigCalc.Application.Dtos;

public class FormFieldLinksDto
{
    public Guid Id { get; set; } = default!;
    public RecordFieldDto FormField { get; set; } = default!;
    public ICollection<RecordFieldDto> ReceiptFields { get; set; } = [];
}