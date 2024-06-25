using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Entities;
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
        string key = "cart";

        var result = await cacheService.GetAsync(
            key,
            async () =>
            {
                var items = new List<CartItems>
                {
                    new CartItems(request.BookId, request.Quantity)
                };

                var cart = new AddItemToCartCartEvent(
                    Guid.NewGuid(),
                    items);

                await publisher.Publish(cart, cancellationToken);

                return cart;
            }, cancellationToken);

        if(result is not null)
        {
            result.Items.Add(new CartItems(request.BookId, request.Quantity));

            await publisher.Publish(result, cancellationToken);

            await cacheService.SetAsync(key, result, cancellationToken);
        }

        return Result.Success();
    }
}

public sealed class CartItems
{
    public CartItems(Guid bookId, int quantity)
    {
        BookId = bookId;
        Quantity = quantity;
    }

    public Guid BookId { get; init; }
    public int Quantity { get; init; }
}