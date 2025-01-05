using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities.Base.Records;

namespace SiradigCalc.Infra.Configurations.Records;

public abstract class BaseRecordValueConfiguration<TRecord, TRecordId, TField, TRecordTemplate, TRecordSection, TValue> : IEntityTypeConfiguration<TValue>
    where TRecordTemplate : BaseRecordTemplate<TRecordSection, TField>
    where TRecordSection : BaseRecordSection<TField>
    where TRecord : BaseRecordInstance<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>
    where TValue : BaseRecordValue<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>
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
