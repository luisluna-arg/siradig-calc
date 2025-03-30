using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Infra.Configurations;

public abstract class RecordTemplateLinkConfiguration : IEntityTypeConfiguration<RecordTemplateLink>
{
    public virtual void Configure(EntityTypeBuilder<RecordTemplateLink> builder)
    {
        builder
            .Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("gen_random_uuid()");

        builder
            .HasOne(r => r.LeftTemplate)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(r => r.RightTemplate)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
