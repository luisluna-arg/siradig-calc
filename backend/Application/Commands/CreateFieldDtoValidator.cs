using FluentValidation;
using SiradigCalc.Application.Dtos;

namespace SiradigCalc.Application.Commands;

public class CreateFieldDtoValidator : AbstractValidator<CreateFieldDto>
{
    private const short LABEL_SIZE = 100;

    public CreateFieldDtoValidator()
    {
        RuleFor(f => f.Label)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Field label is required.")
            .MaximumLength(100)
            .WithMessage($"Field label must not exceed {LABEL_SIZE} characters.");

        RuleFor(f => f.IsRequired)
            .NotNull()
            .WithMessage("Field 'IsRequired' must be specified.");
    }
}
