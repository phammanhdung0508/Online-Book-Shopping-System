using Application.Cart.Dto;
using MediatR;

namespace Application.Cart.Commands.RemoveItemFromCart;

public sealed record RemoveItemFromCartEvent(
    Guid Id,
    List<CartItems> Items) : INotification;