using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mappers.Base;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Application.Mappers;

public class FormValueMapper(IDtoMapperManager dtoMapperManager)
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

public class ReceiptValueMapper(IDtoMapperManager dtoMapperManager)
    : BaseMapper<ReceiptValue, FieldValueDto>(dtoMapperManager),
    IReceiptValueMapper
{
    public override FieldValueDto Map(ReceiptValue source)
        => new FieldValueDto()
        {
            RecordId = source.RecordId,
            FieldId = source.FieldId,
            Value = source.Value
        };
}

public interface IReceiptValueMapper : IDtoMapper<ReceiptValue, FieldValueDto>
{
}
