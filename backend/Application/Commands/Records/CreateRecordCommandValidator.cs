using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Records;

public abstract class CreateRecordCommandValidator<TCommand, TValueDto, TRecordTemplate, TRecordSection, TField>
    : AbstractValidator<TCommand>
    where TCommand : CreateRecordCommand<Guid, TValueDto>
    where TField : BaseRecordField, new()
    where TRecordSection : BaseRecordSection<TField>, new()
    where TRecordTemplate : BaseRecordTemplate<TRecordSection, TField>, new()
{
    private const short TITLE_SIZE = 100;
    private const short DESCRIPTION_SIZE = 200;

    public CreateRecordCommandValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage($"Name must not exceed {TITLE_SIZE} characters.");

        RuleFor(c => c.TemplateId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("TemplateId is required.")
            .MustAsync(async (templateId, cancellationToken) =>
                await dbContext.Set<TRecordTemplate>().AnyAsync(t => t.Id == templateId))
            .WithMessage("The provided TemplateId does not exist in the database.");

        RuleFor(c => c.Values)
            .NotNull()
            .WithMessage("Values cannot be null.")
            .Must(values => values.Count > 0)
            .WithMessage("Values must contain at least one item.")
            .ForEach(valueRule => valueRule.NotNull().WithMessage("Each value must not be null."));
    }
}
