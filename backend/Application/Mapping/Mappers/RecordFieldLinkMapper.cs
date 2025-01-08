using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Mappers;

public class RecordFieldLinkMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<RecordFieldLink, RecordFieldLinkDto>(dtoMapperManager),
    IRecordFieldLinkMapper
{
    public override RecordFieldLinkDto Map(RecordFieldLink source)
        => new RecordFieldLinkDto()
        {
            TemplateLinkId = source.TemplateLinkId,
            FormField = DtoMapperManager.Map<RecordFieldDto>(source.FormField),
            ReceiptField = DtoMapperManager.Map<RecordFieldDto>(source.ReceiptField)
        };
}

public interface IRecordFieldLinkMapper : IDtoMapper<RecordFieldLink, RecordFieldLinkDto>
{
}
