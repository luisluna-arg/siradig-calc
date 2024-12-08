using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities.Forms;

namespace SiradigCalc.Infra.Configurations.Forms;

public class FormTemplateConfiguration : IEntityTypeConfiguration<FormTemplate>
{
    public void Configure(EntityTypeBuilder<FormTemplate> builder)
    {
    }
}