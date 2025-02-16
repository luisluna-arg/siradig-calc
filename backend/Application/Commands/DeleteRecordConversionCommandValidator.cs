using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class DeleteRecordConversionCommandValidator : AbstractValidator<DeleteRecordConversionCommand>
{
    public DeleteRecordConversionCommandValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c.SourceId)
            .NotEmpty()
            .MustAsync(async (sourceId, cancel) => await dbContext.Records
                .AnyAsync(o => o.Id == sourceId, cancel))
            .WithMessage("Source record needs to exist.");

        RuleFor(c => c.ConversionId)
            .NotEmpty()
            .MustAsync(async (conversionId, cancel) => await dbContext.RecordConversions
                .AnyAsync(o => o.Id == conversionId, cancel))
            .WithMessage("Conversion record needs to exist.");
    }
}
