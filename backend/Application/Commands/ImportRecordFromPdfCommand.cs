using System.Globalization;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Dtos.Import;
using SiradigCalc.Application.Helpers.Pdf;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class ImportRecordFromPdfCommand : IRequest<RecordImportResultDto>
{
    public IFormFile File { get; set; } = default!;
    public Guid TemplateId { get; set; }
}

public class ImportRecordFromPdfCommandHandler(
    ISolutionDbContext dbContext,
    IReceiptPdfTextExtractor textExtractor)
    : IRequestHandler<ImportRecordFromPdfCommand, RecordImportResultDto>
{
    public async Task<RecordImportResultDto> Handle(ImportRecordFromPdfCommand request, CancellationToken cancellationToken)
    {
        var template = await dbContext.RecordTemplates
            .Include(t => t.Sections)
            .ThenInclude(s => s.Fields)
            .FirstOrDefaultAsync(t => t.Id == request.TemplateId, cancellationToken);

        if (template is null)
            throw new ValidationException([new ValidationFailure(nameof(request.TemplateId), $"Template '{request.TemplateId}' was not found.")]);

        var lines = await ExtractLines(request.File, cancellationToken);
        var valuesByLabel = MatchLabels(lines, template);

        var values = template.Sections
            .SelectMany(s => s.Fields)
            .Where(field => valuesByLabel.ContainsKey(Normalize(field.Label)))
            .Select(field => new CreateValueDto
            {
                FieldId = field.Id,
                Value = valuesByLabel[Normalize(field.Label)].ToString(CultureInfo.InvariantCulture)
            })
            .ToList();

        return new RecordImportResultDto { TemplateId = template.Id, Values = values };
    }

    private async Task<IReadOnlyList<string>> ExtractLines(IFormFile file, CancellationToken cancellationToken)
    {
        // PdfPig requires a seekable stream, so buffer the upload into memory first.
        using var buffer = new MemoryStream();
        await using (var upload = file.OpenReadStream())
        {
            await upload.CopyToAsync(buffer, cancellationToken);
        }
        buffer.Position = 0;
        return textExtractor.ExtractLines(buffer);
    }

    private static Dictionary<string, decimal> MatchLabels(IReadOnlyList<string> lines, RecordTemplate template)
    {
        var labels = template.Sections
            .SelectMany(s => s.Fields)
            .Select(f => Normalize(f.Label))
            .Distinct()
            .Select(label => (Label: label, Tokens: label.Split(' ', StringSplitOptions.RemoveEmptyEntries)))
            .ToList();

        var result = new Dictionary<string, decimal>();

        foreach (var line in lines)
        {
            var tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var normalized = tokens.Select(Normalize).ToArray();

            for (int i = 0; i < tokens.Length; i++)
            {
                foreach (var (label, labelTokens) in labels)
                {
                    if (result.ContainsKey(label)) continue;
                    if (i + labelTokens.Length >= tokens.Length) continue;

                    bool matches = true;
                    for (int j = 0; j < labelTokens.Length; j++)
                    {
                        if (normalized[i + j] != labelTokens[j]) { matches = false; break; }
                    }

                    if (matches && ReceiptAmountParser.TryParse(tokens[i + labelTokens.Length], out var value))
                        result[label] = value;
                }
            }
        }

        return result;
    }

    private static string Normalize(string s) => s.Trim().ToLowerInvariant();
}
