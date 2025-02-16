using SiradigCalc.Core.Entities.Base;

namespace SiradigCalc.Core.Entities;

public class RecordTemplate : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<RecordTemplateSection> Sections { get; set; } = [];
    public ICollection<RecordTemplateLink> Links { get; set; } = [];
}
