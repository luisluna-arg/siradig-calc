using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class CreateRecordValueCommandValidator : AbstractValidator<CreateRecordValueCommand>
{
    private const short NAME_SIZE = 100;

    public CreateRecordValueCommandValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c.FieldId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("FieldId is required.")
            .MustAsync(async (fieldId, cancellationToken) =>
                await dbContext.RecordTemplateFields.AnyAsync(f => f.Id == fieldId, cancellationToken))
            .WithMessage("The provided FieldId does not exist in the database.");

        RuleFor(c => c.Value)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Value is required.")
            .MaximumLength(100)
            .WithMessage($"Value must not exceed {NAME_SIZE} characters.");
    }
}
