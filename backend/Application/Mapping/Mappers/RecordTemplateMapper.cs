using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Mappers;

public class RecordTemplateMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<RecordTemplate, RecordTemplateDto>(dtoMapperManager),
    IRecordTemplateMapper
{
    public override RecordTemplateDto Map(RecordTemplate source)
        => new RecordTemplateDto()
        {
            Name = source.Name,
            Description = source.Description,
            Sections = DtoMappingService.Map<RecordTemplateSectionDto>(source.Sections)
        };
}

public interface IRecordTemplateMapper : IDtoMapper<RecordTemplate, RecordTemplateDto>
{
}