using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mappers.Base;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Application.Mappers;

public class FormTemplateMapper(IDtoMapperManager dtoMapperManager)
    : BaseMapper<FormTemplate, RecordTemplateDto>(dtoMapperManager),
    IFormTemplateMapper
{
    public override RecordTemplateDto Map(FormTemplate source)
        => new RecordTemplateDto()
        {
            Name = source.Name,
            Description = source.Description,
            Sections = DtoMapperManager.Map<RecordTemplateSectionDto>(source.Sections)
        };
}

public interface IFormTemplateMapper : IDtoMapper<FormTemplate, RecordTemplateDto>
{
}

public class ReceiptTemplateMapper(IDtoMapperManager dtoMapperManager)
    : BaseMapper<ReceiptTemplate, RecordTemplateDto>(dtoMapperManager),
    IReceiptTemplateMapper
{
    public override RecordTemplateDto Map(ReceiptTemplate source)
        => new RecordTemplateDto()
        {
            Name = source.Name,
            Description = source.Description,
            Sections = DtoMapperManager.Map<RecordTemplateSectionDto>(source.Sections)
        };
}

public interface IReceiptTemplateMapper : IDtoMapper<ReceiptTemplate, RecordTemplateDto>
{
}