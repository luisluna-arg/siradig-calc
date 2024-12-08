using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SiradigCalc.Core.Entities.Base;

namespace SiradigCalc.Core.Entities;

public class FieldTypeMapping : Entity<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public override int Id { get; set; } = default!;
    public required string Name { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
}
