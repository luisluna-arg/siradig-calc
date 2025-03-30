using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public record UpdateRecordTemplateCommand : IRequest<Guid>
{
    [JsonIgnore]
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<UpdateSectionDto> Sections { get; set; } = [];
}

public class UpdateRecordTemplateCommandHandler(ISolutionDbContext dbContext)
    : IRequestHandler<UpdateRecordTemplateCommand, Guid>
{
    public async virtual Task<Guid> Handle(UpdateRecordTemplateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var updateDate = DateTime.UtcNow;

            var recordTemplate = await dbContext.RecordTemplates
                .Include(r => r.Sections)
                    .ThenInclude(r => r.Fields)
                .FirstAsync(r => r.Id == command.Id, cancellationToken);

            recordTemplate.Name = command.Name;
            recordTemplate.Description = command.Description;

            var sectionIds = command.Sections.Where(s => s.Id.HasValue).Select(s => s.Id).ToHashSet();

            var sectionsToRemove = recordTemplate.Sections.Where(f => !sectionIds.Contains(f.Id)).ToList();
            dbContext.RecordTemplateSections.RemoveRange(sectionsToRemove);

            foreach (var sectionData in command.Sections.Where(s => !s.Id.HasValue))
            {
                recordTemplate.Sections.Add(new RecordTemplateSection
                {
                    Name = sectionData.Name,
                    Fields = sectionData.Fields.Select(f => new RecordTemplateField()
                    {
                        Label = f.Label,
                        FieldType = f.FieldType,
                        IsRequired = f.IsRequired
                    }).ToList()
                });
            }

            foreach (var sectionData in command.Sections.Where(s => s.Id.HasValue && recordTemplate.Sections.Any(s1 => s1.Id == s.Id.Value)))
            {
                var section = dbContext.RecordTemplateSections.FirstOrDefault(s => s.Id == sectionData.Id);
                if (section == null) continue;

                section.Name = sectionData.Name;

                var fieldIds = sectionData.Fields.Where(s => s.Id.HasValue).Select(s => s.Id).ToHashSet();

                var fieldsToRemove = section.Fields.Where(f => !fieldIds.Contains(f.Id)).ToList();
                dbContext.RecordTemplateFields.RemoveRange(fieldsToRemove);

                foreach (var fieldData in sectionData.Fields.Where(s => !s.Id.HasValue))
                {
                    section.Fields.Add(new RecordTemplateField
                    {
                        Label = fieldData.Label,
                        FieldType = fieldData.FieldType,
                        IsRequired = fieldData.IsRequired
                    });
                }

                foreach (var fieldData in sectionData.Fields.Where(f => f.Id.HasValue && section.Fields.Any(f1 => f1.Id == f.Id.Value)))
                {
                    var field = dbContext.RecordTemplateFields.FirstOrDefault(f => f.Id == fieldData.Id);
                    if (field == null) continue;

                    field.Label = fieldData.Label;
                    field.FieldType = fieldData.FieldType;
                    field.IsRequired = fieldData.IsRequired;
                }
            }

            await dbContext.SaveChangesAsync();

            return recordTemplate.Id;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            foreach (var item in ex.Entries)
            {
                System.Diagnostics.Debug.WriteLine($"{item.Entity}");
            }
            throw;
        }
    }
}
