using Application.Users.Commands.DeleteUser;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/users")]
public sealed class UsersController : ApiController
{
    public UsersController(ISender sender)
        : base(sender)
    { }

    [MapToApiVersion(1)]
    [HttpDelete("remove")]
    public async Task<IActionResult> Delete(
        CancellationToken cancellationToken, 
        [FromBody] DeleteUserCommand command)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }
}