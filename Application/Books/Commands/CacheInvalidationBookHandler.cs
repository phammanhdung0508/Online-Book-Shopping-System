using Application.Abstractions.Caching;
using Application.Books.Commands.CreateBook;
using Application.Books.Commands.DeleteBook;
using Application.Books.Commands.UpdateBook;
using MediatR;

namespace Application.Books.Commands;

internal sealed class CacheInvalidationBookHandler :
    INotificationHandler<BookCreatedEvent>,
    INotificationHandler<BookUpdatedEvent>,
    INotificationHandler<BookDeletedEvent>
{
    private readonly ICacheService cacheService;

    public CacheInvalidationBookHandler(
        ICacheService cacheService)
    {
        this.cacheService = cacheService;
    }

    public Task Handle(BookCreatedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.Id, cancellationToken);
    }

    public Task Handle(BookDeletedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.Id, cancellationToken);
    }

    public Task Handle(BookUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.Id, cancellationToken);
    }

    private async Task HandleInternal(Guid userId, CancellationToken cancellationToken)
    {
        await cacheService.RemoveAsync("books", cancellationToken);
    }
}
