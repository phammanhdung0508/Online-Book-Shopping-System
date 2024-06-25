namespace Application.Cart.Queries.GetCart;

public sealed record GetCartResponse(
    Guid Id,
    List<CartItems> Items);

public sealed class CartItems
{
    public CartItems(Guid bookId, int quantity)
    {
        BookId = bookId;
        Quantity = quantity;
    }

    public Guid BookId { get; init; }
    public int Quantity { get; init; }
}