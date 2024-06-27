using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Application.Books.Queries.GetBookById;
using Domain.Abstractions.IRepository;
using Domain.Errors;
using Domain.Shared;

namespace Application.Orders.Queries.GetOrderById;

internal sealed class GetOrderByIdQueryHandler
    : IQueryHandler<GetOrderByIdQuery, GetOrderByIdResponse>
{
    private readonly ICacheService cacheService;
    private readonly IOrderRepository orderRepository;
    private readonly IOrderDetailRepository orderDetailRepository;

    public GetOrderByIdQueryHandler(
        ICacheService cacheService, 
        IOrderRepository orderRepository,
        IOrderDetailRepository orderDetailRepository)
    {
        this.cacheService = cacheService;
        this.orderRepository = orderRepository;
        this.orderDetailRepository = orderDetailRepository;
    }

    public async Task<Result<GetOrderByIdResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id);

        var result = await cacheService.GetAsync<GetOrderByIdResponse>(
            $"orders-{id}",
            async () =>
            {
                var order = await orderRepository.GetById(id);

                if (order is not null)
                {
                    var list = orderDetailRepository.GetByOrderId(order.Id);

                    var items = list.Select(od => new OrderItem(od.Id, od.Quantity)).ToList();

                    var response = new GetOrderByIdResponse(
                        order.Id,
                        order.OrderDate,
                        items);

                    return response;
                }

                return null;
            }, cancellationToken);

        if (result is not null)
        {
            return result;
        }

        return Result.Failure<GetOrderByIdResponse>(Error.NullValue);
    }
}
