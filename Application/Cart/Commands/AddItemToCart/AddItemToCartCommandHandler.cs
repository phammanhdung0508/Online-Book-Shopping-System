using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Errors;
using Domain.Shared;
using MediatR;

namespace Application.Cart.Commands.AddItemToCart;

internal sealed class AddItemToCartCommandHandler : ICommandHandler<AddItemToCartCommand>
{
    private readonly IPublisher publisher;
    private readonly ICacheService cacheService;

    public AddItemToCartCommandHandler(
        IPublisher publisher,
        ICacheService cacheService)
    {
        this.publisher = publisher;
        this.cacheService = cacheService;
    }

    public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        var result = await cacheService.GetAsync(
            "cart",
            async () =>
            {
                var items = new List<CartItems>
                {
                    new CartItems(request.BookId, request.Quantity)
                };

                var cart = new AddItemToCartEvent(
                    Guid.NewGuid(),
                    items);

                await publisher.Publish(cart, cancellationToken);

                return cart;
            }, cancellationToken);

        if(item is not null)
        {
            await publisher.Publish(result, cancellationToken);

            if (result.Items.Count != 1)
            {
                result.Items.Remove(item);
                await cacheService.SetAsync("cart", result, cancellationToken);
            }

            return Result.Success();
        }
        else
        {
            return Result.Failure(CartErrors.ItemNotFound);
        }
    }
}
