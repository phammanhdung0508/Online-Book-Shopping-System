using Application.Abstractions.Messaging;

namespace Application.Orders.Queries.GetOrderById;

public sealed record GetOrderByIdQuery(Guid Id) : IQuery<GetOrderByIdResponse>;