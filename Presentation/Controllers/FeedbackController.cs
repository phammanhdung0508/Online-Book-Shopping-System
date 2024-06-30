using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Feedbacks.Queries.GetFeedbacks;
using Application.Feedbacks.Queries.GetFeedbackById;
using Application.Feedbacks.Commands.CreateFeedback;
using Application.Feedbacks.Commands.UpdateFeedback;
using Application.Feedbacks.Commands.DeleteFeedback;
using Asp.Versioning;

namespace Presentation.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/feedbacks")]
public sealed class FeedbackController : ApiController
{
    public FeedbackController(ISender sender) : base(sender) { }

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
        var query = new GetFeedbacksQuery
            (pageIndex, pageSize, filter, sort, sortBy, null);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [MapToApiVersion(1)]
    [HttpGet("detail")]
    public async Task<IActionResult> GetById(
        GetFeedbackByIdQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [MapToApiVersion(1)]
    [HttpPost("add")]
    public async Task<IActionResult> Add(
        [FromBody] FeedbackCreatedCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [MapToApiVersion(1)]
    [HttpPut("edit")]
    public async Task<IActionResult> Edit(
        [FromBody] FeedbackUpdatedCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [MapToApiVersion(1)]
    [HttpDelete("remove")]
    public async Task<IActionResult> Remove(
        [FromBody] FeedbackDeletedCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }
}