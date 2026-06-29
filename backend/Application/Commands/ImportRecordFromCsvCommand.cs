using System.Globalization;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos.Import;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class ImportRecordFromCsvCommand : IRequest<CsvRecordImportResultDto>
{
    public IFormFile File { get; set; } = default!;
    public Guid TemplateId { get; set; }
}

public class ImportRecordFromCsvCommandHandler(ISolutionDbContext dbContext)
    : IRequestHandler<ImportRecordFromCsvCommand, CsvRecordImportResultDto>
{
    private const char Separator = ';';
    public async Task<CsvRecordImportResultDto> Handle(ImportRecordFromCsvCommand request, CancellationToken cancellationToken)
    {
        var rows = await ParseCsv(request.File, cancellationToken);

        var valuesByKey = rows
            .GroupBy(r => (Normalize(r.Section), Normalize(r.Label)))
            .ToDictionary(g => g.Key, g => g.First().Value);

        var template = await dbContext.RecordTemplates
            .Include(t => t.Sections)
            .ThenInclude(s => s.Fields)
            .FirstOrDefaultAsync(t => t.Id == request.TemplateId, cancellationToken);

        if (template is null)
            throw new ValidationException([new ValidationFailure(nameof(request.TemplateId), $"Template '{request.TemplateId}' was not found.")]);

        return new CsvRecordImportResultDto
        {
            TemplateId = template.Id,
            Sections = template.Sections.Select(section => new CsvSectionImportResultDto
            {
                SectionId = section.Id,
                Name = section.Name,
                Fields = section.Fields.Select(field => new CsvFieldValueDto
                {
                    FieldId = field.Id,
                    Label = field.Label,
                    Value = valuesByKey.TryGetValue((Normalize(section.Name), Normalize(field.Label)), out var v) ? v : null
                }).ToList()
            }).ToList()
        };
    }

    private static async Task<IReadOnlyList<CsvRow>> ParseCsv(IFormFile file, CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
        var rows = new List<CsvRow>();
        bool isHeader = true;

        string? line;
        while ((line = await reader.ReadLineAsync(cancellationToken)) is not null)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            if (isHeader) { isHeader = false; continue; }

            var fields = SplitLine(line);
            if (fields.Length < 3) continue;

            if (!decimal.TryParse(fields[2].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
                continue;

            rows.Add(new CsvRow(fields[0].Trim(), fields[1].Trim(), value));
        }

        return rows;
    }

    private static string[] SplitLine(string line) => line.Split(Separator).Select(f => f.Trim('"')).ToArray();

    private static string Normalize(string s) => s.Trim().ToLowerInvariant();

    private sealed record CsvRow(string Section, string Label, decimal Value);
}
