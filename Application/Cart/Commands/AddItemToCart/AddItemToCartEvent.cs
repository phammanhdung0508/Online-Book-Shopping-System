using Application.Cart.Dto;
using MediatR;

namespace Application.Cart.Commands.AddItemToCart;

public sealed record AddItemToCartEvent(
    Guid Id,
    List<CartItems> Items) : INotification;