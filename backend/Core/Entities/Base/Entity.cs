using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SiradigCalc.Core.Entities.Interfaces;

namespace SiradigCalc.Core.Entities.Base;

public abstract class Entity<T>() : IAuditable, IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual T Id { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool Deleted { get; set; }
}

public interface IEntity { }

public abstract class Entity() : Entity<Guid>()
{
}