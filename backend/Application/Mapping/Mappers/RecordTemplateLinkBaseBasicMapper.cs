using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Mappers;

public class RecordTemplateLinkBaseBasicMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<RecordTemplateLink, RecordTemplateLinkBasicDto>(dtoMapperManager),
    IRecordTemplateLinkBasicMapper
{
    public override RecordTemplateLinkBasicDto Map(RecordTemplateLink source)
        => new RecordTemplateLinkBasicDto()
        {
            Id = source.Id,
            SourceTemplateId = source.FormTemplateId,
            TargetTemplateId = source.ReceiptTemplateId,
        };
}

public interface IRecordTemplateLinkBasicMapper : IDtoMapper<RecordTemplateLink, RecordTemplateLinkBasicDto>
{
}