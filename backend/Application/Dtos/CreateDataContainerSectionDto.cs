namespace SiradigCalc.Application.Dtos;

public class CreateSectionDto
{
    public string Name { get; set; } = string.Empty;
    public IEnumerable<CreateFieldDto> Fields { get; set; } = [];
}
