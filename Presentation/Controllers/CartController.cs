﻿using Application.Cart.Commands.RemoveItemFromCart;
using Application.Cart.Commands.AddItemToCart;
using Application.Cart.Queries.GetCart;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Cart.Commands.CartCompletion;
using Asp.Versioning;

namespace Presentation.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/cart")]
public sealed class CartController : ApiController
{
    public CartController(ISender sender) : base(sender) { }

    [MapToApiVersion(1)]
    [HttpGet("")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetCartQuery(), cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [MapToApiVersion(1)]
    [HttpPost("completion")]
    public async Task<IActionResult> Completion(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new CartCompletionCommand(), cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [MapToApiVersion(1)]
    [HttpPost("add-item")]
    public async Task<IActionResult> Create(
        [FromBody] AddItemToCartCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [MapToApiVersion(1)]
    [HttpDelete("remove-item")]
    public async Task<IActionResult> Delete(
        [FromBody] RemoveItemFromCartCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }
}
