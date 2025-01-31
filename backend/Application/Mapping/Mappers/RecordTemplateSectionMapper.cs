using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Mappers;

public class RecordTemplateSectionMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<RecordTemplateSection, RecordTemplateSectionDto>(dtoMapperManager),
    IRecordTemplateSectionMapper
{
    public override RecordTemplateSectionDto Map(RecordTemplateSection source)
        => new RecordTemplateSectionDto()
        {
            Name = source.Name,
            Fields = DtoMappingService.Map<RecordTemplateFieldDto>(source.Fields)
        };
}

public interface IRecordTemplateSectionMapper : IDtoMapper<RecordTemplateSection, RecordTemplateSectionDto>
{
}