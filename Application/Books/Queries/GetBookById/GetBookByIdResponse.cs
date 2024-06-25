namespace Application.Books.Queries.GetBookById;

public sealed record GetBookByIdResponse(
    Guid Id,
    string Title);
