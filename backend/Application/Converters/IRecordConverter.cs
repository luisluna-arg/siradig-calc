using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Converters;

public interface IRecordConverter
{
    Task<Record> Convert(Record source, RecordTemplate target, CancellationToken cancellationToken);
}