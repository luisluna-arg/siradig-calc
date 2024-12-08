using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Infra.Configurations.Receipts;

public class ReceiptFieldConfiguration : IEntityTypeConfiguration<ReceiptField>
{
    public void Configure(EntityTypeBuilder<ReceiptField> builder)
    {
    }
}