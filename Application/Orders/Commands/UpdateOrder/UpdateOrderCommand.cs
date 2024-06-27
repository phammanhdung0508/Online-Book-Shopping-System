using Application.Abstractions.Messaging;

namespace Application.Orders.Commands.UpdateOrder;

public sealed record UpdateOrderCommand(
    Guid OrderDetailId,
    int Quantiy) : ICommand;