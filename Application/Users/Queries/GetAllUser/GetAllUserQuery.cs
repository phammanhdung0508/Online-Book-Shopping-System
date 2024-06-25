using Application.Abstractions.Messaging;

namespace Application.Users.Queries.GetAllUser;

public sealed record GetAllUserQuery() : IQuery<List<UserResponse>>
{
}