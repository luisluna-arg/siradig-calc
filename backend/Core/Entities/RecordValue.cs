using SiradigCalc.Core.Entities.Base;

namespace SiradigCalc.Core.Entities;

public class RecordValue : Entity
{
    public Guid RecordId { get; set; }
    public Record Record { get; set; } = null!;
    public Guid FieldId { get; set; }
    public RecordTemplateField Field { get; set; } = null!;
    public string Value { get; set; } = string.Empty;
}
