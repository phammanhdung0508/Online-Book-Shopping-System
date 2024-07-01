using Application.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Asp.Versioning;
using Application.Users.Commands.LoginUser;

namespace Presentation.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/authentication")]
public sealed class AuthenController : ApiController
{
    public AuthenController(ISender sender) : base(sender) { }

    [MapToApiVersion(1)]
    [HttpPost("login")]
    public async Task<IActionResult> Login
        ([FromBody] LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        var response = await Sender.Send(command, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [MapToApiVersion(1)]
    [HttpPost("register")]
    public async Task<IActionResult> Register
        ([FromBody] CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }
}
