using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Application.Mappers;

public class ReceiptTemplateSectionMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<ReceiptTemplateSection, RecordTemplateSectionDto>(dtoMapperManager),
    IReceiptTemplateSectionMapper
{
    public override RecordTemplateSectionDto Map(ReceiptTemplateSection source)
        => new RecordTemplateSectionDto()
        {
            Name = source.Name,
            Fields = DtoMappingService.Map<RecordFieldDto>(source.Fields)
        };
}

public interface IReceiptTemplateSectionMapper : IDtoMapper<ReceiptTemplateSection, RecordTemplateSectionDto>
{
}