namespace SiradigCalc.Application.Dtos;

public class UpdateSectionDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<UpdateFieldDto> Fields { get; set; } = [];
}
