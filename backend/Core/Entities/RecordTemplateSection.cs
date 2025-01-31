using SiradigCalc.Core.Entities.Base;

namespace SiradigCalc.Core.Entities;

public class RecordTemplateSection : Entity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<RecordTemplateField> Fields { get; set; } = new List<RecordTemplateField>();
}
