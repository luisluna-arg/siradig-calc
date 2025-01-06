using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mappers.Base;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Core.Entities.Receipts;

namespace SiradigCalc.Application.Mappers;

public class FormMapper(IDtoMapperManager dtoMapperManager)
    : BaseMapper<Form, RecordDto>(dtoMapperManager),
    IFormMapper
{
    public override RecordDto Map(Form source)
        => new RecordDto()
        {
            RecordTemplateId = source.RecordTemplateId,
            Values = DtoMapperManager.Map<FieldValueDto>(source.Values)
        };
}

public interface IFormMapper : IDtoMapper<Form, RecordDto>
{
}

public class ReceiptMapper(IDtoMapperManager dtoMapperManager)
    : BaseMapper<Receipt, RecordDto>(dtoMapperManager),
    IReceiptMapper
{
    public override RecordDto Map(Receipt source)
        => new RecordDto()
        {
            RecordTemplateId = source.RecordTemplateId,
            Values = DtoMapperManager.Map<FieldValueDto>(source.Values)
        };
}

public interface IReceiptMapper : IDtoMapper<Receipt, RecordDto>
{
}