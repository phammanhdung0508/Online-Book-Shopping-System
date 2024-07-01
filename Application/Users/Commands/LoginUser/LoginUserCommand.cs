using Application.Abstractions.Messaging;

namespace Application.Users.Commands.LoginUser;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<LoginUserResponse>;