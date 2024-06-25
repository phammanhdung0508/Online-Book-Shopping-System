using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Application.Cart.Queries.GetCart;
using Domain.Errors;
using Domain.Shared;
using MediatR;

namespace Application.Cart.Commands.RemoveItemFromCart;

internal sealed class RemoveItemFromCartCommandHandler 
    : ICommandHandler<RemoveItemFromCartCommand>
{
    private readonly IPublisher publisher;
    private readonly ICacheService cacheService;

    public RemoveItemFromCartCommandHandler(IPublisher publisher, ICacheService cacheService)
    {
        this.publisher = publisher;
        this.cacheService = cacheService;
    }

    public async Task<Result> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
    {
        var result = await cacheService.GetAsync<RemoveItemFromCartEvent>(
            "cart", cancellationToken);

        if (result is null)
        {
            return Result.Failure(CartErrors.NotFound);
        }

        var item = result.Items
            .Where(x => x.BookId == request.BookId)
            .First();

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
