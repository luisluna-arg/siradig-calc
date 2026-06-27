using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos.Import;
using SiradigCalc.Application.Helpers.Pdf;
using SiradigCalc.Core.Entities;
using SiradigCalc.Core.Entities.Enums;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class ImportRecordFromPdfCommand : IRequest<PdfImportResultDto>
{
    /// <summary>The payroll receipt PDF to parse.</summary>
    public IFormFile File { get; set; } = default!;

    /// <summary>When true, create a <see cref="RecordTemplate"/> if none matches.</summary>
    public bool GenerateTemplate { get; set; }
}

public class ImportRecordFromPdfCommandHandler(
    ISolutionDbContext dbContext,
    IReceiptPdfTextExtractor textExtractor,
    IReceiptDocumentParser documentParser)
    : IRequestHandler<ImportRecordFromPdfCommand, PdfImportResultDto>
{
    public async Task<PdfImportResultDto> Handle(ImportRecordFromPdfCommand request, CancellationToken cancellationToken)
    {
        var sections = await ExtractSections(request.File, cancellationToken);

        var extractedLabels = sections
            .SelectMany(s => s.Entries.Select(e => Normalize(e.Label)))
            .ToHashSet();

        var templates = await dbContext.RecordTemplates
            .Include(t => t.Sections)
            .ThenInclude(s => s.Fields)
            .ToListAsync(cancellationToken);

        var result = new PdfImportResultDto { Sections = sections.ToList() };

        var match = FindMatch(templates, extractedLabels);
        if (match != null)
        {
            result.TemplateMatch = new TemplateMatchDto
            {
                Found = true,
                TemplateId = match.Id,
                TemplateName = match.Name
            };

            return result;
        }

        if (request.GenerateTemplate && sections.Count > 0)
        {
            var created = await CreateTemplate(sections, cancellationToken);
            result.TemplateMatch = new TemplateMatchDto
            {
                Found = false,
                Created = true,
                TemplateId = created.Id,
                TemplateName = created.Name
            };
        }

        return result;
    }

    private async Task<IReadOnlyList<ImportedSectionDto>> ExtractSections(IFormFile file, CancellationToken cancellationToken)
    {
        // PdfPig requires a seekable stream, so buffer the upload into memory first.
        using var buffer = new MemoryStream();
        await using (var upload = file.OpenReadStream())
        {
            await upload.CopyToAsync(buffer, cancellationToken);
        }

        buffer.Position = 0;
        var lines = textExtractor.ExtractLines(buffer);

        return documentParser.Parse(lines);
    }

    private static RecordTemplate? FindMatch(IEnumerable<RecordTemplate> templates, HashSet<string> extractedLabels)
    {
        if (extractedLabels.Count == 0)
        {
            return null;
        }

        foreach (var template in templates)
        {
            var templateLabels = template.Sections
                .SelectMany(s => s.Fields.Select(f => Normalize(f.Label)))
                .ToHashSet();

            if (extractedLabels.IsSubsetOf(templateLabels))
            {
                return template;
            }
        }

        return null;
    }

    private async Task<RecordTemplate> CreateTemplate(IReadOnlyList<ImportedSectionDto> sections, CancellationToken cancellationToken)
    {
        var template = new RecordTemplate
        {
            Name = $"Imported Template {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}",
            Description = "Created from an imported PDF receipt.",
            Sections = sections.Select(section => new RecordTemplateSection
            {
                Name = section.Name,
                Fields = section.Entries.Select(entry => new RecordTemplateField
                {
                    Label = entry.Label,
                    FieldType = FieldType.Number,
                    IsRequired = false
                }).ToList()
            }).ToList()
        };

        dbContext.RecordTemplates.Add(template);
        await dbContext.SaveChangesAsync(cancellationToken);

        return template;
    }

    private static string Normalize(string label) => label.Trim().ToLowerInvariant();
}
