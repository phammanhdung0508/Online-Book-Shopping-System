using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Abstractions.IRepository;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using MediatR;

namespace Application.Cart.Commands.CartCompletion;

public sealed class CartCompletionCommandHandler
    : ICommandHandler<CartCompletionCommand>
{
    private readonly IPublisher publisher;
    private readonly ICacheService cacheService;
    private readonly IOrderRepository orderRepository;
    private readonly IOrderDetailRepository orderDetailRepository;
    private readonly IUnitOfWork unitOfWork;

    public CartCompletionCommandHandler
        (IPublisher publisher,
        ICacheService cacheService,
        IOrderRepository orderRepository,
        IOrderDetailRepository orderDetailRepository,
        IUnitOfWork unitOfWork)
    {
        this.publisher = publisher;
        this.cacheService = cacheService;
        this.orderRepository = orderRepository;
        this.orderDetailRepository = orderDetailRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CartCompletionCommand request, CancellationToken cancellationToken)
    {
        var result = await cacheService.GetAsync<CartCompletionEvent>(
            "cart", cancellationToken);

        if (result is null)
        {
            return Result.Failure(CartErrors.NotFound);
        }
        else
        {
            var orderDetails = result.Items
                        .Select(od => new OrderDetail.Builder()
                        .SetOrderId(od.BookId)
                        .SetOrderId(result.Id)
                        .SetQuantity(od.Quantity)
                        .Build()).ToList();

            var order = new Order.Builder()
                .SetID(result.Id)
                .SetOrderDate(DateTime.UtcNow)
                .Build();

            orderRepository.Create(order);
            orderDetailRepository.Create(orderDetails);

            await Task.WhenAll(
                unitOfWork.SaveChangeAsync(cancellationToken),
                publisher.Publish(result, cancellationToken));

            return Result.Success(result);
        }
    }
}