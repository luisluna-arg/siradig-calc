namespace SiradigCalc.Application.Dtos.Import;

public class CsvSectionImportResultDto
{
    public Guid SectionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<CsvFieldValueDto> Fields { get; set; } = [];
}
