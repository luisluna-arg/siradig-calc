using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities.Forms;

namespace SiradigCalc.Application.Mappers;

public class FormMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<Form, RecordDto>(dtoMapperManager),
    IFormMapper
{
    public override RecordDto Map(Form source)
        => new RecordDto()
        {
            RecordTemplateId = source.RecordTemplateId,
            Values = DtoMappingService.Map<FieldValueDto>(source.Values)
        };
}

public interface IFormMapper : IDtoMapper<Form, RecordDto>
{
}