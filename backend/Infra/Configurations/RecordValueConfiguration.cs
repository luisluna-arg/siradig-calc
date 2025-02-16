using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Infra.Configurations;

public abstract class RecordValueConfiguration : IEntityTypeConfiguration<RecordValue>
{
    public virtual void Configure(EntityTypeBuilder<RecordValue> builder)
    {
        builder
            .HasOne(r => r.Field)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(r => r.Record)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
