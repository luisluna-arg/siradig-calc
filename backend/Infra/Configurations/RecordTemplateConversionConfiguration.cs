using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Infra.Configurations;

public class RecordTemplateConversionConfiguration : IEntityTypeConfiguration<RecordTemplateConversion>
{
    public void Configure(EntityTypeBuilder<RecordTemplateConversion> builder)
    {
        builder
            .HasOne(rtc => rtc.RecordTemplateLink)
            .WithMany()
            .HasForeignKey(rtc => rtc.RecordTemplateLinkId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(rtc => rtc.Source)
            .WithMany(r => r.ConvertedTo)
            .HasForeignKey(rtc => rtc.SourceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(rtc => rtc.Target)
            .WithMany(r => r.ConvertedFrom)
            .HasForeignKey(rtc => rtc.TargetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}