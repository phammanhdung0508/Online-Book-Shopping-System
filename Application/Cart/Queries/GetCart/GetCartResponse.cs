using Application.Cart.Dto;

namespace Application.Cart.Queries.GetCart;

public sealed record GetCartResponse(
    Guid Id,
    List<CartItems> Items);