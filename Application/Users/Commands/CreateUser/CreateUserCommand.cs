using Application.Abstractions.Messaging;

namespace Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand
    (string email, 
    string password) : ICommand;