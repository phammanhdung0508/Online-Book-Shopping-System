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
        var result = await cacheService.GetAsync<GetCartResponse>(
            "cart", cancellationToken);

        if (result is null)
        {
            return Result.Failure(CartErrors.NotFound);
        }

        await publisher.Publish(new RemoveItemFromCartEvent(request.Id), cancellationToken);

        return Result.Success();
    }
}
