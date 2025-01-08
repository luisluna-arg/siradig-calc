using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Application.Mappers;

public class ReceiptFieldMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<ReceiptField, RecordFieldDto>(dtoMapperManager),
    IReceiptFieldMapper
{
    public override RecordFieldDto Map(ReceiptField source)
        => new RecordFieldDto()
        {
            Id = source.Id,
            Label = source.Label,
            FieldType = DtoMapperManager.Map<FieldTypeDto>(source.FieldType),
            IsRequired = source.IsRequired
        };
}

public interface IReceiptFieldMapper : IDtoMapper<ReceiptField, RecordFieldDto>
{
}