using SiradigCalc.Core.Entities.Base.DataContainers;

namespace SiradigCalc.Core.Entities.Receipts;

public class Receipt() : BaseDataContainerInstance<ReceiptTemplate, Guid, ReceiptTemplateSection, ReceiptValue, ReceiptField>()
{
}
