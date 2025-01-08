using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities.Forms;

namespace SiradigCalc.Application.Mappers;

public class FormValueMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<FormValue, FieldValueDto>(dtoMapperManager),
    IFormValueMapper
{
    public override FieldValueDto Map(FormValue source)
        => new FieldValueDto()
        {
            RecordId = source.RecordId,
            FieldId = source.FieldId,
            Value = source.Value
        };
}

public interface IFormValueMapper : IDtoMapper<FormValue, FieldValueDto>
{
}