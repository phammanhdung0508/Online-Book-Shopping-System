using MediatR;

namespace Application.Orders.Commands.PatchOrder;

public sealed record PatchOrderEvent(Guid Id) : INotification;