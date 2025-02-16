using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Infra.Configurations.Records;

public class RecordTemplateFieldConfiguration : IEntityTypeConfiguration<RecordTemplateField>
{
    public void Configure(EntityTypeBuilder<RecordTemplateField> builder)
    {
        builder
            .HasMany(b => b.Links)
            .WithOne()
            .OnDelete(DeleteBehavior.Restrict);
    }
}