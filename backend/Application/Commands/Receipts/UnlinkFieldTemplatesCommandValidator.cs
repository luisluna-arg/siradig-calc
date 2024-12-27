using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class UnlinkFieldTemplatesCommandValidator(ISolutionDbContext dbContext) : LinkFieldTemplatesCommandValidator(dbContext)
{
}