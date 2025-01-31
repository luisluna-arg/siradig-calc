using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Mappers;

public class RecordFieldLinkMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<RecordTemplateFieldLink, RecordTemplateFieldLinkDto>(dtoMapperManager),
    IRecordFieldLinkMapper
{
    public override RecordTemplateFieldLinkDto Map(RecordTemplateFieldLink source)
        => new RecordTemplateFieldLinkDto()
        {
            TemplateLinkId = source.TemplateLinkId,
            RightTemplateField = DtoMappingService.Map<RecordTemplateFieldDto>(source.RightField),
            LeftTemplateField = DtoMappingService.Map<RecordTemplateFieldDto>(source.LeftField)
        };
}

public interface IRecordFieldLinkMapper : IDtoMapper<RecordTemplateFieldLink, RecordTemplateFieldLinkDto>
{
}
