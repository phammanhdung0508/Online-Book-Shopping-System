namespace Domain.Errors;

public static class BookErrors
{
    public static readonly Error NotFound = new("Books.NotFound", "Can't not found a request book.");
    public static readonly Error InvalidYear = new("Books.InvalidYear", "This year is not valid.");
}