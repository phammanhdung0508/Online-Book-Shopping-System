using Application.Abstractions.Messaging;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Orders.Commands.PatchOrder;

public sealed record PatchOrderCommand(
    Guid OrderDetailId,
    JsonPatchDocument<OrderDetail> patchDoc) : ICommand;