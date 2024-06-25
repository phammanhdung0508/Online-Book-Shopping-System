using Application.Cart.Commands.RemoveItemFromCart;
using Application.Cart.Commands.AddItemToCart;
using Application.Cart.Queries.GetCart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/cart")]
public sealed class CartController : ApiController
{
    public CartController(ISender sender) : base(sender) { }

    [HttpGet("")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetCartQuery(), cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }
    
    [HttpPost("add-item")]
    public async Task<IActionResult> Create(
        [FromBody] AddItemToCartCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [HttpDelete("remove-item")]
    public async Task<IActionResult> Delete(
        [FromBody] RemoveItemFromCartCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }
}
