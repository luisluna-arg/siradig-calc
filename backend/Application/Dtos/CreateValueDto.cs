namespace SiradigCalc.Application.Dtos;

public class CreateValueDto
{
    public Guid FieldId { get; set; }
    public string Value { get; set; } = null!;
}