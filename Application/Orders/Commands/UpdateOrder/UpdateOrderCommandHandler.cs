using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Abstractions.IRepository;
using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.Orders.Commands.UpdateOrder;

internal sealed class UpdateOrderCommandHandler
    : ICommandHandler<UpdateOrderCommand>
{
    private readonly ICacheService cacheService;
    private readonly IPublisher publisher;
    private readonly IUnitOfWork unitOfWork;
    private readonly IOrderDetailRepository orderDetailRepository;

    public UpdateOrderCommandHandler(
        ICacheService cacheService, 
        IPublisher publisher, 
        IUnitOfWork unitOfWork, 
        IOrderDetailRepository orderDetailRepository)
    {
        this.cacheService = cacheService;
        this.publisher = publisher;
        this.unitOfWork = unitOfWork;
        this.orderDetailRepository = orderDetailRepository;
    }

    public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderDetail = new OrderDetail.Builder()
            .SetOrderId(request.OrderDetailId)
            .SetQuantity(request.Quantiy)
            .Build();

        orderDetailRepository.Update(orderDetail);

        await Task.WhenAll(
            unitOfWork.SaveChangeAsync(cancellationToken),
            publisher.Publish(
                new UpdateOrderEvent(
                    request.OrderDetailId
            ), cancellationToken));

        return Result.Success();
    }
}
