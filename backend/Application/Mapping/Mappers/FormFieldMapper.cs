using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities.Forms;

namespace SiradigCalc.Application.Mappers;

public class FormFieldMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<FormField, RecordFieldDto>(dtoMapperManager),
    IFormFieldMapper
{
    public override RecordFieldDto Map(FormField source)
        => new RecordFieldDto()
        {
            Label = source.Label,
            FieldType = DtoMapperManager.Map<FieldTypeDto>(source.FieldType),
            IsRequired = source.IsRequired
        };
}

public interface IFormFieldMapper : IDtoMapper<FormField, RecordFieldDto>
{
}