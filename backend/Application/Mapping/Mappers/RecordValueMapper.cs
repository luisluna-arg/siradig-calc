using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Mappers;

public class RecordValueMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<RecordValue, RecordValueDto>(dtoMapperManager),
    IRecordValueMapper
{
    public override RecordValueDto Map(RecordValue source)
        => new RecordValueDto()
        {
            FieldId = source.FieldId,
            Label = source.Field.Label,
            Value = source.Value
        };
}

public interface IRecordValueMapper : IDtoMapper<RecordValue, RecordValueDto>
{
}