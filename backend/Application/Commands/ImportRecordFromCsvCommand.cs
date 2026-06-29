using System.Globalization;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Dtos.Import;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class ImportRecordFromCsvCommand : IRequest<RecordImportResultDto>
{
    public IFormFile File { get; set; } = default!;
    public Guid TemplateId { get; set; }
}

public class ImportRecordFromCsvCommandHandler(ISolutionDbContext dbContext)
    : IRequestHandler<ImportRecordFromCsvCommand, RecordImportResultDto>
{
    private const char Separator = ';';
    public async Task<RecordImportResultDto> Handle(ImportRecordFromCsvCommand request, CancellationToken cancellationToken)
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

        var values = template.Sections
            .SelectMany(section => section.Fields
                .Where(field => valuesByKey.TryGetValue((Normalize(section.Name), Normalize(field.Label)), out _))
                .Select(field => new CreateValueDto
                {
                    FieldId = field.Id,
                    Value = valuesByKey[(Normalize(section.Name), Normalize(field.Label))].ToString(CultureInfo.InvariantCulture)
                }))
            .ToList();

        return new RecordImportResultDto { TemplateId = template.Id, Values = values };
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
