using SiradigCalc.Core.Entities.Base;

namespace SiradigCalc.Core.Entities;

public class RecordTemplateFieldLink : Entity
{
    public Guid TemplateLinkId { get; set; }
    public RecordTemplateLink TemplateLink { get; set; } = default!;
    public Guid RightFieldId { get; set; }
    public RecordTemplateField RightField { get; set; } = default!;
    public Guid LeftFieldId { get; set; }
    public RecordTemplateField LeftField { get; set; } = default!;
}