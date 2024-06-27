using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Application.Cart.Dto;
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

        if(result is null)
        {
            return Result.Failure(CartErrors.NotFound);
        }
        else
        {
            var item = result.Items
            .Any(x => x.BookId == request.BookId);

            if (item is false)
            {
                result.Items.Add(new CartItems(request.BookId, request.Quantity));

                await publisher.Publish(result, cancellationToken);
                await cacheService.SetAsync("cart", result, cancellationToken);

                return Result.Success();
            }
            else
            {
                return Result.Failure(CartErrors.ItemExist);
            }
        }
    }
}