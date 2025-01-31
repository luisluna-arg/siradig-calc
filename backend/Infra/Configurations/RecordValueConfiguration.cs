using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Infra.Configurations;

public abstract class RecordValueConfiguration : IEntityTypeConfiguration<RecordValue>
{
    public virtual void Configure(EntityTypeBuilder<RecordValue> builder)
    {
    }
}
