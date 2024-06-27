using Application.Abstractions.Caching;
using Application.Cart.Commands.AddItemToCart;
using Application.Cart.Commands.RemoveItemFromCart;
using MediatR;

namespace Application.Cart.Commands;

internal sealed class CacheInvalidationCartHandler :
    INotificationHandler<AddItemToCartEvent>,
    INotificationHandler<RemoveItemFromCartEvent>
{
    private readonly ICacheService cacheService;

    public CacheInvalidationCartHandler(ICacheService cacheService)
    {
        this.cacheService = cacheService;
    }

    public Task Handle(RemoveItemFromCartEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.Id, cancellationToken);
    }

    public Task Handle(AddItemToCartEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.Id, cancellationToken);
    }

    private async Task HandleInternal(Guid userId, CancellationToken cancellationToken)
    {
        await cacheService.RemoveAsync("cart", cancellationToken);
    }
}
