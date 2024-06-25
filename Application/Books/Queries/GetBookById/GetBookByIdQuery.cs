using Application.Abstractions.Messaging;
using Application.Books.Queries.GetBookById;

namespace Application.Books.Queries.GetAllBook;

public sealed record GetBookByIdQuery(string Id) : IQuery<GetBookByIdResponse>;