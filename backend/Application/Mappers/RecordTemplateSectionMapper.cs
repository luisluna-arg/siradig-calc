using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mappers.Base;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Application.Mappers;

public class FormTemplateSectionMapper(IDtoMapperManager dtoMapperManager)
    : BaseMapper<FormTemplateSection, RecordTemplateSectionDto>(dtoMapperManager),
    IFormTemplateSectionMapper
{
    public override RecordTemplateSectionDto Map(FormTemplateSection source)
        => new RecordTemplateSectionDto()
        {
            Name = source.Name,
            Fields = DtoMapperManager.Map<RecordFieldDto>(source.Fields)
        };
}

public interface IFormTemplateSectionMapper : IDtoMapper<FormTemplateSection, RecordTemplateSectionDto>
{
}

public class ReceiptTemplateSectionMapper(IDtoMapperManager dtoMapperManager)
    : BaseMapper<ReceiptTemplateSection, RecordTemplateSectionDto>(dtoMapperManager),
    IReceiptTemplateSectionMapper
{
    public override RecordTemplateSectionDto Map(ReceiptTemplateSection source)
        => new RecordTemplateSectionDto()
        {
            Name = source.Name,
            Fields = DtoMapperManager.Map<RecordFieldDto>(source.Fields)
        };
}

public interface IReceiptTemplateSectionMapper : IDtoMapper<ReceiptTemplateSection, RecordTemplateSectionDto>
{
}