using Application.Abstractions.Messaging;

namespace Application.Users.Queries.GetUsers;

public sealed record GetUsersQuery(
    int PageIndex,
    int PageSize,
    string? Filter,
    string? Sort,
    string? SortBy,
    string? IncludeProperties) : IQuery<List<GetUsersResponse>>;