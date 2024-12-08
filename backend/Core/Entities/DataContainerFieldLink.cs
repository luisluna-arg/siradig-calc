using SiradigCalc.Core.Entities.Base;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Core.Entities;

public class DataContainerFieldLink : Entity
{
    public Guid FormFieldId { get; set; }
    public FormField FormField { get; set; } = default!;
    public Guid ReceiptFieldId { get; set; }
    public ReceiptField ReceiptField { get; set; } = default!;
}