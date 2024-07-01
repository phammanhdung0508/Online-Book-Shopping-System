namespace Application.Users.Queries.GetUsers;

public sealed record GetUsersResponse(
    Guid Id,
    string Email,
    string Role,
    string Token);