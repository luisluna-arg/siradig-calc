using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mappers.Base;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Application.Mappers;

public class FormFieldMapper(IDtoMapperManager dtoMapperManager)
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

public class ReceiptFieldMapper(IDtoMapperManager dtoMapperManager)
    : BaseMapper<ReceiptField, RecordFieldDto>(dtoMapperManager),
    IReceiptFieldMapper
{
    public override RecordFieldDto Map(ReceiptField source)
        => new RecordFieldDto()
        {
            Label = source.Label,
            FieldType = DtoMapperManager.Map<FieldTypeDto>(source.FieldType),
            IsRequired = source.IsRequired
        };
}

public interface IReceiptFieldMapper : IDtoMapper<ReceiptField, RecordFieldDto>
{
}
