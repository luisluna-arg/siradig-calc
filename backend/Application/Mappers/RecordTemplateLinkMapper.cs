using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mappers.Base;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Mappers;

public class RecordTemplateLinkMapper(IDtoMapperManager dtoMapperManager)
    : BaseMapper<RecordTemplateLink, RecordTemplateLinkDto>(dtoMapperManager),
    IRecordTemplateLinkMapper
{
    public override RecordTemplateLinkDto Map(RecordTemplateLink source)
        => new RecordTemplateLinkDto()
        {
            FormTemplate = DtoMapperManager.Map<RecordTemplateDto>(source.FormTemplate),
            ReceiptTemplate = DtoMapperManager.Map<RecordTemplateDto>(source.ReceiptTemplate),
            RecordFieldLinks = DtoMapperManager.Map<RecordFieldLinkDto>(source.RecordFieldLinks)
        };
}

public interface IRecordTemplateLinkMapper : IDtoMapper<RecordTemplateLink, RecordTemplateLinkDto>
{
}