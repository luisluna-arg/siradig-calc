using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class CreateRecordCommandValidator : AbstractValidator<CreateRecordCommand>
{
    private const short TITLE_SIZE = 100;

    public CreateRecordCommandValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(100)
            .WithMessage($"Title must not exceed {TITLE_SIZE} characters.");

        RuleFor(c => c.TemplateId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("TemplateId is required.")
            .MustAsync(async (templateId, cancellationToken) =>
                await dbContext.RecordTemplates.AnyAsync(t => t.Id == templateId))
            .WithMessage("The provided TemplateId does not exist.");

        RuleFor(c => c.Values)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Values cannot be null.")
            .Must(values => values.Count > 0)
            .WithMessage("Values must contain at least one item.")
            .ForEach(valueRule => valueRule.NotNull().WithMessage("Each value must not be null."))
            .ForEach(valueRule => valueRule.MustAsync(
                (fieldValue, cancellationToken) => dbContext.RecordTemplateFields
                    .AnyAsync(f => f.Id == fieldValue.FieldId, cancellationToken))
                    .WithMessage("{PropertyName} need to exist under the record template requested"));

        RuleFor(c => c)
            .MustAsync(async (c, cancellationToken) =>
                await dbContext.RecordTemplates
                    .Include(t => t.Sections)
                        .ThenInclude(t => t.Fields)
                    .AnyAsync(t => t.Id == c.TemplateId &&
                        c.Values.Select(v => v.FieldId).All(
                            fId => t.Sections.SelectMany(s => s.Fields).Any(f => f.Id == fId)
                            ), cancellationToken))
            .WithMessage("All fields need to exist under any section of the pretended template");
    }
}
