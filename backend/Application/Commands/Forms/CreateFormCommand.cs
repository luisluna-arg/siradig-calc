using SiradigCalc.Application.Commands.Records;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class CreateFormCommand()
    : CreateRecordCommand<CreateValueDto>()
{
}

public class CreateFormHandler(ISolutionDbContext dbContext)
    : CreateRecordCommandHandler<CreateFormCommand, Form, FormTemplate, FormTemplateSection, FormField, FormValue>(dbContext)
{
}

public class CreateFormCommandValidator(ISolutionDbContext dbContext)
    : CreateRecordCommandValidator<CreateFormCommand, CreateValueDto, FormTemplate, FormTemplateSection, FormField>(dbContext)
{
}