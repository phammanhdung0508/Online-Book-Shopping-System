namespace Application.Users.Queries;

public sealed record UserResponse(
    Guid Id, 
    string Email,
    string Role,
    string Token);