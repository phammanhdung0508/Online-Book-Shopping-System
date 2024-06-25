using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Errors;
using Domain.Shared;
using MediatR;

namespace Application.Cart.Queries.GetCart;

internal class GetCartQueryHandler :
    IQueryHandler<GetCartQuery, GetCartResponse>
{
    private readonly IPublisher publisher;
    private readonly ICacheService cacheService;

    public GetCartQueryHandler(IPublisher publisher, ICacheService cacheService)
    {
        this.publisher = publisher;
        this.cacheService = cacheService;
    }

    public async Task<Result<GetCartResponse>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var result = await cacheService.GetAsync<GetCartResponse>(
            "cart", cancellationToken);

        if(result is not null)
        {
            return result;
        }

        return Result.Failure<GetCartResponse>(CartErrors.NotFound);
    }
}
