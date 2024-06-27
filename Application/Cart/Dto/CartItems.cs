namespace Application.Cart.Dto;

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