using SiradigCalc.Application.Converters.Strategies;
using SiradigCalc.Application.Dtos.Conversion;
using SiradigCalc.Core.Entities.Base.Records;

namespace SiradigCalc.Application.Converters;

public class RecordConverter : IRecordConverter
{
    private readonly IEnumerable<IRecordConverterStrategy> _strategies;

    public RecordConverter(IEnumerable<IRecordConverterStrategy> strategies)
    {
        _strategies = strategies;
    }

    public async Task<Record> Convert(Record source, RecordTemplate target, CancellationToken cancellationToken)
    {
        var strategy = _strategies.FirstOrDefault(s => s.CanConvert(source.GetType(), target.GetType()));

        if (strategy != null)
        {
            return await strategy.Convert(source, target, cancellationToken);
        }

        throw new InvalidOperationException($"No converter found for {source.GetType().Name} to {target.GetType().Name}.");
    }
}
