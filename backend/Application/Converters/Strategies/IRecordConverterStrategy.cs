using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Converters.Strategies;

public interface IRecordConverterStrategy
{
    bool CanConvert(Type sourceType, Type targetType);

    Task<Record> Convert(Record source, RecordTemplate target, CancellationToken cancellationToken);
}