using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SiradigCalc.Api.Controllers.Base;

public abstract class BaseController : ControllerBase
{
    public BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }

    public IMediator Mediator { get; }
}