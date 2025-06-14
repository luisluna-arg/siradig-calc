using FluentValidation;
using Microsoft.VisualBasic.FileIO;

namespace SiradigCalc.Application.Commands;

public class CreateRecordFieldCommandValidator : AbstractValidator<CreateRecordTemplateFieldCommand>
{
    private const short LABEL_SIZE = 200;

    public CreateRecordFieldCommandValidator()
    {
        RuleFor(c => c.Label)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Label is required.")
            .MaximumLength(200)
            .WithMessage($"Label must not exceed {LABEL_SIZE} characters.");

        RuleFor(c => c.FieldType)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("FieldType is required.")
            .Must(fieldType => Enum.IsDefined(typeof(FieldType), fieldType))
            .WithMessage("Invalid FieldType value.");

        RuleFor(c => c.IsRequired)
            .NotNull()
            .WithMessage("IsRequired must be specified.");
    }
}
