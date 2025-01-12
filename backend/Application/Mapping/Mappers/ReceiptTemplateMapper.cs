using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Application.Mappers;

public class ReceiptTemplateMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<ReceiptTemplate, RecordTemplateDto>(dtoMapperManager),
    IReceiptTemplateMapper
{
    public override RecordTemplateDto Map(ReceiptTemplate source)
        => new RecordTemplateDto()
        {
            Name = source.Name,
            Description = source.Description,
            Sections = DtoMappingService.Map<RecordTemplateSectionDto>(source.Sections)
        };
}

public interface IReceiptTemplateMapper : IDtoMapper<ReceiptTemplate, RecordTemplateDto>
{
}