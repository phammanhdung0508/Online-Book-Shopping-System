using MediatR;

namespace Application.Users.Commands.DeleteUser;

public record UserDeletedEvent : INotification
{
    public Guid Id { get; init; }
}