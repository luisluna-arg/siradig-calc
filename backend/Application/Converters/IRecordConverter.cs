using SiradigCalc.Application.Dtos.Conversion;
using SiradigCalc.Core.Entities.Base.Records;

namespace SiradigCalc.Application.Converters;

public interface IRecordConverter
{
    Task<Record> Convert(Record source, RecordTemplate target, CancellationToken cancellationToken);
}