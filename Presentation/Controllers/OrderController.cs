using Application.Orders.Queries.GetOrderById;
using Application.Orders.Queries.GetOrders;
using Application.Orders.Commands.PatchOrder;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Domain.Entities;
using Asp.Versioning;

namespace Presentation.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/orders")]
public sealed class OrderController : ApiController
{
    public OrderController(ISender sender) : base(sender) { }

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
        var result = await Sender.Send(new GetOrdersQuery(
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
        [FromBody] GetOrderByIdQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [MapToApiVersion(1)]
    [HttpPatch("detail/{orderDetailId}/{quantity}")]
    public async Task<IActionResult> JsonPatch(
        string orderDetailId,
        [FromBody] JsonPatchDocument<OrderDetail> patchDoc,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(
            new PatchOrderCommand(
                Guid.Parse(orderDetailId), patchDoc), cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }
}