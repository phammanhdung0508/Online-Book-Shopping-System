namespace Domain.Errors;

public static class CartErrors
{
    public static readonly Error NotFound = new("Cart.NotFound", "Can't not found any cart.");
    public static readonly Error ItemExist = new("Cart.ItemExist", "This book already exist in cart.");
}
