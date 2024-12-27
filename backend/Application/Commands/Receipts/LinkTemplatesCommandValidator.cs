using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class LinkTemplatesCommandValidator : AbstractValidator<LinkTemplatesCommand>
{
    public LinkTemplatesCommandValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c.ReceiptTemplateId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("ReceiptTemplateId is required.")
            .MustAsync(async (receiptTemplateId, cancellationToken) =>
                await dbContext.ReceiptTemplates.AnyAsync(rt => rt.Id == receiptTemplateId))
            .WithMessage("The provided ReceiptTemplateId does not exist in the database.");

        RuleFor(c => c.FormTemplateId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("FormTemplateId is required.")
            .MustAsync(async (formTemplateId, cancellationToken) =>
                await dbContext.FormTemplates.AnyAsync(ft => ft.Id == formTemplateId))
            .WithMessage("The provided FormTemplateId does not exist in the database.");

    }
}