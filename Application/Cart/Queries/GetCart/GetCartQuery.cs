using Application.Abstractions.Messaging;

namespace Application.Cart.Queries.GetCart;

public sealed record GetCartQuery : IQuery<GetCartResponse>;