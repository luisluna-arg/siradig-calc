using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiradigCalc.Core.Entities.Base.Records;

namespace SiradigCalc.Infra.Configurations.Forms;

public class RecordConversionBaseConfiguration<TConversion, TSource, TTarget> : IEntityTypeConfiguration<TConversion>
    where TSource: Record 
    where TTarget: Record
    where TConversion: RecordConversionBase<TSource, TTarget>
{
    public void Configure(EntityTypeBuilder<TConversion> builder)
    {
        builder.UseTptMappingStrategy();
    }
}