namespace SiradigCalc.Application.Dtos.Import;

public class TemplateMatchDto
{
    /// <summary>True when an existing template matched the extracted labels.</summary>
    public bool Found { get; set; }

    /// <summary>True when a new template was created from the extracted document.</summary>
    public bool Created { get; set; }

    public Guid? TemplateId { get; set; }
    public string? TemplateName { get; set; }
}
