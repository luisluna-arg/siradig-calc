using SiradigCalc.Core.Entities.Base.Records;

namespace SiradigCalc.Application.Converters.Strategies;

public interface IRecordConverterStrategy
{
    bool CanConvert(Type sourceType, Type targetType);

    Task<Record> Convert(Record source, RecordTemplate target, CancellationToken cancellationToken);
}