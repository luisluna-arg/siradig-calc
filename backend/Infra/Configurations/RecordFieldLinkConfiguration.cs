using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Infra.Configurations;

public abstract class RecordFieldLinkConfiguration : IEntityTypeConfiguration<RecordTemplateFieldLink>
{
    public virtual void Configure(EntityTypeBuilder<RecordTemplateFieldLink> builder)
    {
        builder
            .Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("gen_random_uuid()");

        builder
            .HasOne(b => b.LeftField)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(b => b.RightField)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(b => b.TemplateLink)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
