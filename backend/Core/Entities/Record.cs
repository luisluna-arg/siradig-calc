using SiradigCalc.Core.Entities.Base;

namespace SiradigCalc.Core.Entities;

public class Record : Entity
{
    public Guid TemplateId { get; set; } = default!;
    public RecordTemplate Template { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public ICollection<RecordValue> Values { get; set; } = [];
    public ICollection<RecordTemplateConversion> ConvertedTo { get; set; } = [];
    public ICollection<RecordTemplateConversion> ConvertedFrom { get; set; } = [];
}
