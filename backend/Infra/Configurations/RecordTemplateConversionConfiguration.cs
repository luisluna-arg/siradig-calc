using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Infra.Configurations;

public class RecordTemplateConversionConfiguration : IEntityTypeConfiguration<RecordConversion>
{
    public void Configure(EntityTypeBuilder<RecordConversion> builder)
    {
        builder
            .Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("gen_random_uuid()");

        builder
            .HasOne(rtc => rtc.RecordTemplateLink)
            .WithMany()
            .HasForeignKey(rtc => rtc.RecordTemplateLinkId)
            .OnDelete(DeleteBehavior.Restrict);

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