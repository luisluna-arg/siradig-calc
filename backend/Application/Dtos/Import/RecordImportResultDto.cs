namespace SiradigCalc.Application.Dtos.Import;

public class RecordImportResultDto
{
    public Guid TemplateId { get; set; }
    public List<SectionImportResultDto> Sections { get; set; } = [];
}
