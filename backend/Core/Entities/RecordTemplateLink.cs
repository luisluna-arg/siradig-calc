using SiradigCalc.Core.Entities.Base;

namespace SiradigCalc.Core.Entities;

public class RecordTemplateLink : Entity
{
    public Guid RightTemplateId { get; set; }
    public RecordTemplate RightTemplate { get; set; } = default!;
    public Guid LeftTemplateId { get; set; }
    public RecordTemplate LeftTemplate { get; set; } = default!;
    public ICollection<RecordTemplateFieldLink> RecordFieldLinks { get; set; } = [];
}