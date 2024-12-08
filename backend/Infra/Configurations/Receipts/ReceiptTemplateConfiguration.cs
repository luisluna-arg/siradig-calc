using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Infra.Configurations.Receipts;

public class ReceiptTemplateConfiguration : IEntityTypeConfiguration<ReceiptTemplate>
{
    public void Configure(EntityTypeBuilder<ReceiptTemplate> builder)
    {
    }
}