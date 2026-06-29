namespace SiradigCalc.Application.Dtos.Import;

public class RecordImportResultDto
{
    public Guid TemplateId { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<CreateValueDto> Values { get; set; } = [];
}
