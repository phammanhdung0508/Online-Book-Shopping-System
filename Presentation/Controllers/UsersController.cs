using Application.Users.Commands.DeleteUser;
using Application.Users.Queries.GetUserById;
using Application.Users.Queries.GetUsers;
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
    [HttpGet("")]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken,
        [FromQuery] int pageSize,
        [FromQuery] int pageIndex,
        [FromQuery] string? filter = null,
        [FromQuery] string? sort = null,
        [FromQuery] string? sortBy = null)
    {
        var result = await Sender.Send(new GetUsersQuery(
            pageSize,
            pageIndex,
            filter,
            sort,
            sortBy, ""), cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [MapToApiVersion(1)]
    [HttpGet("detail")]
    public async Task<IActionResult> GetById(
        [FromBody] GetUserByIdQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

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