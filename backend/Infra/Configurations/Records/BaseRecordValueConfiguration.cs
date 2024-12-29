using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities.Base.Records;

namespace SiradigCalc.Infra.Configurations.Records;

public abstract class BaseRecordValueConfiguration<TValue, TField> : IEntityTypeConfiguration<TValue>
    where TValue : BaseRecordValue<TField>
    where TField : BaseRecordField
{
    public virtual void Configure(EntityTypeBuilder<TValue> builder)
    {
        builder
            .HasOne(v => v.Field)
            .WithMany()
            .HasForeignKey(v => v.FieldId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
