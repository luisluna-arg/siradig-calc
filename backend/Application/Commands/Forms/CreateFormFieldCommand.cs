using SiradigCalc.Application.Commands.Records;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class CreateFormFieldCommand() : CreateRecordFieldCommand()
{
}

public class CreateFormFieldHandler(ISolutionDbContext dbContext)
    : CreateRecordFieldCommandHandler<CreateFormFieldCommand, FormField>(dbContext)
{
}

public class CreateFormFieldCommandValidator : CreateRecordFieldCommandValidator
{
}