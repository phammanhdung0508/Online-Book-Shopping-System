using Application.Abstractions.Messaging;

namespace Application.Orders.Queries.GetOrderById;

public sealed record GetOrderByIdQuery(string Id) : IQuery<GetOrderByIdResponse>;