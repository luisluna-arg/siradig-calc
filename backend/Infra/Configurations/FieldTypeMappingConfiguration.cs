using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities;
using SiradigCalc.Core.Entities.Enums;

namespace SiradigCalc.Infra.Configurations;

public class FieldTypeMappingConfiguration : IEntityTypeConfiguration<FieldTypeMapping>
{
    public void Configure(EntityTypeBuilder<FieldTypeMapping> builder)
    {
        builder
            .HasData(
                Enum.GetValues(typeof(FieldType))
                    .Cast<FieldType>()
                    .Select(e => new FieldTypeMapping { Id = (int)e, Name = e.ToString()!, Description = e.ToString()! }));
    }
}