using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Mappers;

public class RecordTemplateLinkMapper(IDtoMappingService dtoMapperManager)
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