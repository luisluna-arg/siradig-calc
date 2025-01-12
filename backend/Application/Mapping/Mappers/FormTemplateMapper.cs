using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities.Forms;

namespace SiradigCalc.Application.Mappers;

public class FormTemplateMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<FormTemplate, RecordTemplateDto>(dtoMapperManager),
    IFormTemplateMapper
{
    public override RecordTemplateDto Map(FormTemplate source)
        => new RecordTemplateDto()
        {
            Name = source.Name,
            Description = source.Description,
            Sections = DtoMappingService.Map<RecordTemplateSectionDto>(source.Sections)
        };
}

public interface IFormTemplateMapper : IDtoMapper<FormTemplate, RecordTemplateDto>
{
}