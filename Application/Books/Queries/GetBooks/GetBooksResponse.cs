namespace Application.Books.Queries.GetBooks;

public sealed record GetBooksResponse(
    Guid Id,
    string Title);