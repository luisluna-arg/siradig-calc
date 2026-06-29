namespace SiradigCalc.Application.Dtos.Import;

public class SectionImportResultDto
{
    public Guid SectionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<FieldImportValueDto> Fields { get; set; } = [];
}
