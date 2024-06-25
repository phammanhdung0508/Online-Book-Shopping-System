using MediatR;

namespace Application.Cart.Commands.AddItemToCart;

public sealed record AddItemToCartCartEvent(
    Guid Id,
    List<CartItems> Items) : INotification;