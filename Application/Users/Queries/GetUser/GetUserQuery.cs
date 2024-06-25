using Application.Abstractions.Messaging;

namespace Application.Users.Queries.GetUser;

public sealed record GetUserQuery(string Email, string Password) : IQuery<UserResponse>
{
}