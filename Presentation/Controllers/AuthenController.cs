using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetUser;
using Application.Users.Queries;
using Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Asp.Versioning;

namespace Presentation.Controllers;

[ApiController]
[ApiVersion(1)]
[ApiVersion(2)]
[Route("api/v{v:apiVersion}/authentication")]
public sealed class AuthenController : ApiController
{
    public AuthenController(ISender sender) : base(sender) { }

    [MapToApiVersion(1)]
    [HttpGet("login")]
    public async Task<IActionResult> Login
        ([FromBody] GetUserQuery query,
        CancellationToken cancellationToken)
    {
        Result<UserResponse> response = await Sender.Send(query, cancellationToken);

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
