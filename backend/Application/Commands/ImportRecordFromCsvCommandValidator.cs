using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace SiradigCalc.Application.Commands;

public class ImportRecordFromCsvCommandValidator : AbstractValidator<ImportRecordFromCsvCommand>
{
    public ImportRecordFromCsvCommandValidator()
    {
        RuleFor(c => c.TemplateId)
            .NotEmpty()
            .WithMessage("A template must be selected.");

        RuleFor(c => c.File)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("A CSV file is required.")
            .Must(file => file.Length > 0)
            .WithMessage("The uploaded file is empty.")
            .Must(BeACsv)
            .WithMessage("The uploaded file must be a CSV.");
    }

    private static bool BeACsv(IFormFile file)
    {
        var isCsvContentType =
            string.Equals(file.ContentType, "text/csv", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(file.ContentType, "application/csv", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(file.ContentType, "application/vnd.ms-excel", StringComparison.OrdinalIgnoreCase);

        return isCsvContentType || file.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase);
    }
}
