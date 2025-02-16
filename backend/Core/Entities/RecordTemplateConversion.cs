using SiradigCalc.Core.Entities.Base;

namespace SiradigCalc.Core.Entities;

public class RecordConversion : Entity
{
    public Guid RecordTemplateLinkId { get; set; }
    public RecordTemplateLink RecordTemplateLink { get; set; } = default!;
    public Guid SourceId { get; set; }
    public Record Source { get; set; } = default!;
    public Guid TargetId { get; set; }
    public Record Target { get; set; } = default!;
}