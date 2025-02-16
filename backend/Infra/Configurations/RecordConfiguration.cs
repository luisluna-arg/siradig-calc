using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Infra.Configurations;

public class RecordConfiguration : IEntityTypeConfiguration<Record>
{
    public void Configure(EntityTypeBuilder<Record> builder)
    {
        builder
            .HasMany(r => r.ConvertedTo)
            .WithOne(c => c.Source)
            .HasForeignKey(c => c.SourceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(r => r.ConvertedFrom)
            .WithOne(c => c.Target)
            .HasForeignKey(c => c.TargetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(r => r.Template)
            .WithMany()
            .HasForeignKey(c => c.TemplateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}