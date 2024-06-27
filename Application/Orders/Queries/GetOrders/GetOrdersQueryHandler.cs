using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Abstractions.IRepository;
using Domain.Errors;
using Domain.Shared;

namespace Application.Orders.Queries.GetOrders;

internal sealed class GetOrdersQueryHandler
    : IQueryHandler<GetOrdersQuery, List<GetOrdersResponse>>
{
    private readonly ICacheService cacheService;
    private readonly IOrderRepository orderRepository;

    public GetOrdersQueryHandler(
        ICacheService cacheService, 
        IOrderRepository orderRepository)
    {
        this.cacheService = cacheService;
        this.orderRepository = orderRepository;
    }

    public async Task<Result<List<GetOrdersResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var result = await cacheService.GetAsync<List<GetOrdersResponse>>(
            "orders",
            async () =>
            {
                var list = await orderRepository.Get(
                    request.PageIndex,
                    request.PageSize,
                    request.Filter,
                    request.Sort,
                    request.SortBy,
                    request.IncludeProperties,
                    cancellationToken);

                if (list is not null)
                {
                    var response = new List<GetOrdersResponse>();
                    foreach (var item in list)
                    {
                        response.Add(new GetOrdersResponse(item.Id, item.OrderDate));
                    }
                    return response;
                }

                return null;
            }, cancellationToken);

        if (result is not null)
        {
            return result;
        }

        return Result.Failure<List<GetOrdersResponse>>(Error.NullValue);
    }
}
