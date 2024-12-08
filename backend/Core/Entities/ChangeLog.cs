namespace SiradigCalc.Core.Entities;

public class ChangeLog
{
    public Guid Id { get; set; }
    public string EntityName { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string PrimaryKey { get; set; } = string.Empty;
    public string Changes { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string? User { get; set; }
}
