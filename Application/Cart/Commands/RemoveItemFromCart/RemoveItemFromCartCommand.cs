using Application.Abstractions.Messaging;

namespace Application.Cart.Commands.RemoveItemFromCart;

public sealed record RemoveItemFromCartCommand(Guid BookId) : ICommand;