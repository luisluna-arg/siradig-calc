using SiradigCalc.Core.Entities.Base;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Core.Entities;

public class DataContainerLink : Entity
{
    public Guid FormTemplateId { get; set; }
    public FormTemplate FormTemplate { get; set; } = default!;
    public Guid ReceiptTemplateId { get; set; }
    public ReceiptTemplate ReceiptTemplate { get; set; } = default!;
}