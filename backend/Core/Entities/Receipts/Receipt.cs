using SiradigCalc.Core.Entities.Base.Records;

namespace SiradigCalc.Core.Entities.Receipts;

public class Receipt() : BaseRecordInstance<Receipt, Guid, ReceiptTemplate, ReceiptTemplateSection, ReceiptField, ReceiptValue>()
{
}
