using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities.Base.Records;

namespace SiradigCalc.Infra.Configurations.Forms;

public class RecordTemplateConfiguration : IEntityTypeConfiguration<RecordTemplate>
{
    public void Configure(EntityTypeBuilder<RecordTemplate> builder)
    {
        builder.UseTptMappingStrategy();
    }
}