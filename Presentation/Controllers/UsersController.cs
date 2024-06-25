using Application.Users.Commands.DeleteUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/users")]
public sealed class UsersController : ApiController
{
    public UsersController(ISender sender)
        : base(sender)
    { }

    [HttpDelete("remove")]
    public async Task<IActionResult> Delete(
        CancellationToken cancellationToken, 
        [FromBody] DeleteUserCommand command)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }
}