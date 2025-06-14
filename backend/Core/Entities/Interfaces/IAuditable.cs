namespace SiradigCalc.Core.Entities.Interfaces;

public interface IAuditable
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}
