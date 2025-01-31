using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Infra.Configurations;

public class RecordTemplateSectionConfiguration : IEntityTypeConfiguration<RecordTemplateSection>
{
    public void Configure(EntityTypeBuilder<RecordTemplateSection> builder)
    {
    }
}