namespace Domain.Errors;

public static class OrderErrors
{
    public static readonly Error NotFound = new("Order.NotFound", "Can't not found any orders.");
    public static readonly Error NullPatchDoc = new("Order.NullPatchDoc", "Patch Doc is null.");
    public static readonly Error NotFoundOrderDetail = new("OrderDetail.NotFound", "Can't not found any order detail.");
}