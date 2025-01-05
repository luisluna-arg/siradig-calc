using SiradigCalc.Application.Dtos.Conversion;
using SiradigCalc.Core.Entities.Base.Records;

namespace SiradigCalc.Application.Converters.Strategies;

public interface IRecordConverterStrategy
{
    bool CanConvert(Type sourceType, Type targetType);

    Task<IRecordConversionDto> Convert(Record source, Record target, CancellationToken cancellationToken);
}