namespace Application.Books.Queries.GetBook;

public sealed record GetBookResponse(
    Guid Id,
    string Title);
