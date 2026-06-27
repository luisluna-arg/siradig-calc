namespace SiradigCalc.Application.Dtos.Import;

public class ImportedSectionDto
{
    public string Name { get; set; } = string.Empty;
    public List<ImportedEntryDto> Entries { get; set; } = [];
}
