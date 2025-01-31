using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Infra.Configurations;

public abstract class RecordTemplateLinkConfiguration : IEntityTypeConfiguration<RecordTemplateLink>
{
    public virtual void Configure(EntityTypeBuilder<RecordTemplateLink> builder)
    {
    }
}
