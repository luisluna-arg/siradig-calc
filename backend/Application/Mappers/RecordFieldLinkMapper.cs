using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mappers.Base;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Mappers;

public class RecordFieldLinkMapper(IDtoMapperManager dtoMapperManager)
    : BaseMapper<RecordFieldLink, RecordFieldLinkDto>(dtoMapperManager),
    IRecordFieldLinkMapper
{
    public override RecordFieldLinkDto Map(RecordFieldLink source)
        => new RecordFieldLinkDto()
        {
            TemplateLinkId = source.TemplateLinkId,
            FormField = DtoMapperManager.Map<RecordFieldDto>(source.FormField),
            ReceiptField = DtoMapperManager.Map<RecordFieldDto>(source.FormField)
        };
}

public interface IRecordFieldLinkMapper : IDtoMapper<RecordFieldLink, RecordFieldLinkDto>
{
}
