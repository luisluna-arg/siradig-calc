using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Application.Mappers;

public class ReceiptValueMapper(IDtoMappingService dtoMapperManager)
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