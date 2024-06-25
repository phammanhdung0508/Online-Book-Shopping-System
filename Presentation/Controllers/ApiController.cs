using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender) => Sender = sender;
}