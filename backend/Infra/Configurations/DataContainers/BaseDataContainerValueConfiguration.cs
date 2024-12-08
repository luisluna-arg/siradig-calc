using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities.Base.DataContainers;

public abstract class BaseDataContainerValueConfiguration<TValue, TField> : IEntityTypeConfiguration<TValue>
    where TValue : BaseDataContainerValue<TField>
    where TField : BaseDataContainerField
{
    public virtual void Configure(EntityTypeBuilder<TValue> builder)
    {
        builder
            .HasOne(v => v.Field)
            .WithMany() // Assuming no inverse navigation property
            .HasForeignKey(v => v.FieldId)
            .OnDelete(DeleteBehavior.Restrict); // Optional: Define delete behavior
    }
}
