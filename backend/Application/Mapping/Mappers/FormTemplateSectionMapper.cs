using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities.Forms;

namespace SiradigCalc.Application.Mappers;

public class FormTemplateSectionMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<FormTemplateSection, RecordTemplateSectionDto>(dtoMapperManager),
    IFormTemplateSectionMapper
{
    public override RecordTemplateSectionDto Map(FormTemplateSection source)
        => new RecordTemplateSectionDto()
        {
            Name = source.Name,
            Fields = DtoMappingService.Map<RecordFieldDto>(source.Fields)
        };
}

public interface IFormTemplateSectionMapper : IDtoMapper<FormTemplateSection, RecordTemplateSectionDto>
{
}