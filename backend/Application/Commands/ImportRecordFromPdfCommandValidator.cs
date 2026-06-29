using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace SiradigCalc.Application.Commands;

public class ImportRecordFromPdfCommandValidator : AbstractValidator<ImportRecordFromPdfCommand>
{
    private const string PdfContentType = "application/pdf";

    public ImportRecordFromPdfCommandValidator()
    {
        RuleFor(c => c.TemplateId)
            .NotEmpty()
            .WithMessage("A template must be selected.");

        RuleFor(c => c.File)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("A PDF file is required.")
            .Must(file => file.Length > 0)
            .WithMessage("The uploaded file is empty.")
            .Must(BeAPdf)
            .WithMessage("The uploaded file must be a PDF.");
    }

    private static bool BeAPdf(IFormFile file)
    {
        var isPdfContentType = string.Equals(file.ContentType, PdfContentType, StringComparison.OrdinalIgnoreCase);
        var hasPdfExtension = file.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase);

        return isPdfContentType || hasPdfExtension;
    }
}
