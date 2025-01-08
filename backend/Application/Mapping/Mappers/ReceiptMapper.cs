using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Application.Mappers;

public class ReceiptMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<Receipt, RecordDto>(dtoMapperManager),
    IReceiptMapper
{
    public override RecordDto Map(Receipt source)
        => new RecordDto()
        {
            RecordTemplateId = source.RecordTemplateId,
            Values = DtoMapperManager.Map<FieldValueDto>(source.Values)
        };
}

public interface IReceiptMapper : IDtoMapper<Receipt, RecordDto>
{
}