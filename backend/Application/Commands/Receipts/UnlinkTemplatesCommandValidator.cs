using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class UnlinkTemplatesCommandValidator(ISolutionDbContext dbContext) : LinkTemplatesCommandValidator(dbContext)
{
}