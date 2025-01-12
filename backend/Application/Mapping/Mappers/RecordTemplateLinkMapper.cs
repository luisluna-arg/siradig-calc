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
            FormTemplate = DtoMappingService.Map<RecordTemplateDto>(source.FormTemplate),
            ReceiptTemplate = DtoMappingService.Map<RecordTemplateDto>(source.ReceiptTemplate),
            RecordFieldLinks = source.RecordFieldLinks.GroupBy(l => l.FormFieldId)
                .Select(g =>
                {
                    var formFieldLink = g.First();
                    var formField = formFieldLink.FormField;
                    return new FormFieldLinksDto()
                    {   
                        Id = formFieldLink.Id,
                        FormField = DtoMappingService.Map<RecordFieldDto>(formField),
                        ReceiptFields = DtoMappingService.Map<RecordFieldDto>(g.Select(r => r.ReceiptField).ToArray())
                    };
                }).ToArray()
        };
}

public interface IRecordTemplateLinkMapper : IDtoMapper<RecordTemplateLink, RecordTemplateLinkDto>
{
}