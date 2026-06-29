namespace SiradigCalc.Application.Dtos.Import;

public class FieldImportValueDto
{
    public Guid FieldId { get; set; }
    public string Label { get; set; } = string.Empty;
    public decimal? Value { get; set; }
}
