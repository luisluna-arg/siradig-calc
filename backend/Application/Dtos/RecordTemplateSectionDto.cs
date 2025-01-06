namespace SiradigCalc.Application.Dtos;

public class RecordTemplateSectionDto
{
    public string Name { get; set; } = string.Empty;
    public ICollection<RecordFieldDto> Fields { get; set; } = new List<RecordFieldDto>();
}