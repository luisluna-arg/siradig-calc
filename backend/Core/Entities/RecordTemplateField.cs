using SiradigCalc.Core.Entities.Base;
using SiradigCalc.Core.Entities.Enums;

namespace SiradigCalc.Core.Entities;

public class RecordTemplateField : Entity
{
    public RecordTemplateSection RecordTemplateSection { get; set; } = default!;
    public string Label { get; set; } = string.Empty;
    public FieldType FieldType { get; set; } = default;
    public bool IsRequired { get; set; }
    public ICollection<RecordTemplateFieldLink> Links { get; set; } = [];
}
