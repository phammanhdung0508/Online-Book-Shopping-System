using MediatR;

namespace Application.Orders.Commands.UpdateOrder;

public sealed record UpdateOrderEvent(Guid Id) : INotification;