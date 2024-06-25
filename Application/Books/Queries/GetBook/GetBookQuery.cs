using Application.Abstractions.Messaging;
using Domain.Enum;

namespace Application.Books.Queries.GetBook;

public sealed record GetBookQuery
    (string Filter,
    Sort sort,
    string includeProperties) : IQuery<List<GetBookResponse>>;
