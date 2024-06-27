using Application.Abstractions.Messaging;

namespace Application.Books.Queries.GetBooks;

public sealed record GetBooksQuery
    (int PageIndex,
    int PageSize,
    string? Filter,
    string? Sort,
    string? SortBy,
    string? IncludeProperties) : IQuery<List<GetBooksResponse>>;
