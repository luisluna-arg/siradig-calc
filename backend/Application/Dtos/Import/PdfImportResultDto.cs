namespace SiradigCalc.Application.Dtos.Import;

public class PdfImportResultDto
{
    public TemplateMatchDto TemplateMatch { get; set; } = new();
    public List<ImportedSectionDto> Sections { get; set; } = [];
}
