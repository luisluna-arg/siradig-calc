using SiradigCalc.Application.Dtos.Conversion;
using SiradigCalc.Core.Entities.Base.Records;

namespace SiradigCalc.Application.Converters;

public interface IRecordConverter
{
    Task<IRecordConversionDto> Convert(Record source, Record target, CancellationToken cancellationToken);
}