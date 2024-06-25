using Application.Abstractions.Messaging;

namespace Application.Cart.Commands.AddItemToCart;

public sealed record AddItemToCartCommand(
    Guid BookId,
    int Quantity) : ICommand;