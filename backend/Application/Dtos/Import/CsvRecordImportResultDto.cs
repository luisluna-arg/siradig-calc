namespace SiradigCalc.Application.Dtos.Import;

public class CsvRecordImportResultDto
{
    public Guid TemplateId { get; set; }
    public List<CsvSectionImportResultDto> Sections { get; set; } = [];
}
