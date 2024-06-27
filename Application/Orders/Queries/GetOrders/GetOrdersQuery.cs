using Application.Abstractions.Messaging;

namespace Application.Orders.Queries.GetOrders;

public sealed record GetOrdersQuery(
    int PageIndex,
    int PageSize,
    string? Filter,
    string? Sort,
    string? SortBy,
    string? IncludeProperties) : IQuery<List<GetOrdersResponse>>;
