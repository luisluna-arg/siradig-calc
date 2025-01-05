using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Records;

public abstract class CreateRecordValueCommandValidator<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue> : AbstractValidator<CreateRecordValueCommand>
    where TRecordTemplate : BaseRecordTemplate<TRecordSection, TField>
    where TRecordSection : BaseRecordSection<TField>
    where TRecord : BaseRecordInstance<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>
    where TField : BaseRecordField, new()
    where TValue : BaseRecordValue<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>, new()
{
    private const short NAME_SIZE = 100;

    public CreateRecordValueCommandValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c.FieldId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("FieldId is required.")
            .MustAsync(async (fieldId, cancellationToken) =>
                await dbContext.Set<TValue>().AnyAsync(f => f.Id == fieldId, cancellationToken))
            .WithMessage("The provided FieldId does not exist in the database.");

        RuleFor(c => c.Value)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Value is required.")
            .MaximumLength(100)
            .WithMessage($"Value must not exceed {NAME_SIZE} characters.");
    }
}
