using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Infra.Configurations;

public class RecordTemplateConfiguration : IEntityTypeConfiguration<RecordTemplate>
{
    public void Configure(EntityTypeBuilder<RecordTemplate> builder)
    {
    }
}