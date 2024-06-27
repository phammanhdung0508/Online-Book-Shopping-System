using Application.Cart.Dto;
using MediatR;

namespace Application.Cart.Commands.CartCompletion;

public sealed record CartCompletionEvent(
    Guid Id,
    List<CartItems> Items) : INotification;