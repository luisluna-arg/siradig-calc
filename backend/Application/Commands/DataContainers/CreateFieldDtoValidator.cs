using FluentValidation;
using SiradigCalc.Application.Dtos;

namespace SiradigCalc.Application.Commands.DataContainers;

public class CreateFieldDtoValidator : AbstractValidator<CreateFieldDto>
{
    private const short LABEL_SIZE = 100;
    private const short PLACEHOLDER_SIZE = 200;

    public CreateFieldDtoValidator()
    {
        RuleFor(f => f.Label)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Field label is required.")
            .MaximumLength(100)
            .WithMessage($"Field label must not exceed {LABEL_SIZE} characters.");

        RuleFor(f => f.FieldType)
            .GreaterThan(0)
            .WithMessage("Field type must be greater than 0.");

        RuleFor(f => f.IsRequired)
            .NotNull()
            .WithMessage("Field 'IsRequired' must be specified.");

        RuleFor(f => f.Placeholder)
            .MaximumLength(200)
            .WithMessage($"Placeholder must not exceed {PLACEHOLDER_SIZE} characters.");
    }
}
