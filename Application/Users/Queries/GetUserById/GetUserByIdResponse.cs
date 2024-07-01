namespace Application.Users.Queries.GetUserById;

public sealed record GetUserByIdResponse(
    Guid Id,
    string Email,
    string Phone,
    string Role);