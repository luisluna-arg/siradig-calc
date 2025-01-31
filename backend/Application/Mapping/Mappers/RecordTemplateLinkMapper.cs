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
            Id = source.Id,
            RightTemplate = DtoMappingService.Map<RecordTemplateDto>(source.RightTemplate),
            LeftTemplate = DtoMappingService.Map<RecordTemplateDto>(source.LeftTemplate),
            RecordTemplateFieldLinks = source.RecordFieldLinks.GroupBy(l => l.RightFieldId)
                .Select(g =>
                {
                    var rightFieldLink = g.First();
                    var rightField = rightFieldLink.RightField;
                    return new RecordTemplateFieldLinksDto()
                    {   
                        Id = rightFieldLink.Id,
                        RightField = DtoMappingService.Map<RecordTemplateFieldDto>(rightField),
                        LeftFields = DtoMappingService.Map<RecordTemplateFieldDto>(g.Select(r => r.LeftField).ToArray())
                    };
                }).ToArray()
        };
}

public interface IRecordTemplateLinkMapper : IDtoMapper<RecordTemplateLink, RecordTemplateLinkDto>
{
}