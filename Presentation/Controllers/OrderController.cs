using Application.Orders.Queries.GetOrderById;
using Application.Orders.Queries.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public sealed class OrderController : ApiController
{
    public OrderController(ISender sender) : base(sender) { }

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

    [HttpGet("detail")]
    public async Task<IActionResult> GetById(
        [FromBody] GetOrderByIdQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    /*[HttpPatch("detail/{orderDetailId}")]
    public async Task<IActionResult> Update(
        string orderDetailId,
        [FromBody] JsonPatchDocument)
    {

    }*/
}