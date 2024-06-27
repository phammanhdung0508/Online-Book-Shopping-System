namespace Application.Orders.Queries.GetOrders;

public sealed record GetOrdersResponse(
    Guid Id,
    DateTime OrderDate);