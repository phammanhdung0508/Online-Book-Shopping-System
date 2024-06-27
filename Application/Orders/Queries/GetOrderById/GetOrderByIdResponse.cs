namespace Application.Orders.Queries.GetOrderById;

public sealed record GetOrderByIdResponse(
    Guid Id,
    DateTime OrderDate,
    List<OrderItem> Items);

public sealed class OrderItem { 
    public OrderItem(Guid id, int quantity) {
        Id = id;
        Quantity = quantity;
    }

    public Guid Id { get; init; }
    public int Quantity { get; init; }
}