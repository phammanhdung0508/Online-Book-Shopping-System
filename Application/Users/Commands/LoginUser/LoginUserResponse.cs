namespace Application.Users.Commands.LoginUser;

public sealed record LoginUserResponse(
    Guid Id,
    string Email,
    string Role,
    string Token);