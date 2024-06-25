using MediatR;

namespace Application.Cart.Commands.RemoveItemFromCart;

public sealed record RemoveItemFromCartEvent(Guid Id) : INotification;