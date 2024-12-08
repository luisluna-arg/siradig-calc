using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Dtos;

public class FieldTypeDto
{
    public FieldTypeDto(FieldTypeMapping ftm)
    {
        Id = ftm.Id;
        Name = ftm.Name;
    }

    public int Id { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
}