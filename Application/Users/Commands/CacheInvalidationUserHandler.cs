using Application.Abstractions.Caching;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using MediatR;

namespace Application.Users.Commands;

internal class CacheInvalidationUserHandler :
    INotificationHandler<UserCreatedEvent>,
    INotificationHandler<UserDeletedEvent>
{
    private readonly ICacheService cacheService;

    public CacheInvalidationUserHandler(
        ICacheService cacheService)
    {
        this.cacheService = cacheService;
    }

    public Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.Id, cancellationToken);
    }

    /*public Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.Id, cancellationToken);
    }*/

    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.Id, cancellationToken);
    }

    private async Task HandleInternal(Guid userId, CancellationToken cancellationToken)
    {
        await cacheService.RemoveAsync("books", cancellationToken);
    }
}