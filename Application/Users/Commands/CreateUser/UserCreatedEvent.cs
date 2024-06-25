using MediatR;

namespace Application.Users.Commands.CreateUser;

public sealed record UserCreatedEvent : INotification
{
    public Guid Id { get; init; }
    public string Email { get; init; } = string.Empty;
}
