using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class UpdateFieldDtoValidator : AbstractValidator<UpdateFieldDto>
{
    private const short LABEL_SIZE = 100;

    public UpdateFieldDtoValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c.Id)
            .MustAsync(async (fieldId, cancellationToken) =>
                await dbContext.RecordTemplateFields.AnyAsync(t => t.Id.Equals(fieldId!.Value)))
            .WithMessage("The provided Field Id does not exist.")
            .When(c => c.Id.HasValue);

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
