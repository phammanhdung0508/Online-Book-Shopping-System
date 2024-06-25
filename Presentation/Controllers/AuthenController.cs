using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetUser;
using Application.Users.Queries;
using Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Presentation.Controllers;

[Route("api/authentication")]
public sealed class AuthenController : ApiController
{
    public AuthenController(ISender sender) : base(sender) { }

    [HttpGet("login")]
    public async Task<IActionResult> Login
        ([FromBody] GetUserQuery query,
        CancellationToken cancellationToken)
    {
        Result<UserResponse> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register
        ([FromBody] CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }
}
