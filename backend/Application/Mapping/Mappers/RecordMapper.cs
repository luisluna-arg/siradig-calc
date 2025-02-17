using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Mappers;

public class RecordMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<Record, RecordDto>(dtoMapperManager),
    IRecordMapper
{
    public override RecordDto Map(Record source)
        => new RecordDto()
        {
            Id = source.Id,
            Title = source.Title,
            Name = source.Template?.Name,
            Description = source.Template?.Description,
            RecordTemplateId = source.TemplateId,
            Values = DtoMappingService.Map<RecordValueDto>(source.Values)
        };
}

public interface IRecordMapper : IDtoMapper<Record, RecordDto>
{
}