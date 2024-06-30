using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Abstractions.IRepository;
using Domain.Errors;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Application.Orders.Commands.PatchOrder;

internal sealed class PatchOrderCommandHandler
    : ICommandHandler<PatchOrderCommand>
{
    private readonly IActionContextAccessor actionContextAccessor;
    private readonly IOrderDetailRepository orderDetailRepository;

    public PatchOrderCommandHandler(
        IActionContextAccessor actionContextAccessor,
        IPublisher publisher,
        IUnitOfWork unitOfWork,
        IOrderDetailRepository orderDetailRepository)
    {
        this.actionContextAccessor = actionContextAccessor;
        this.orderDetailRepository = orderDetailRepository;
    }

    public async Task<Result> Handle(PatchOrderCommand request, CancellationToken cancellationToken)
    {
        if (actionContextAccessor.ActionContext is null)
        {
            return Result.Failure(
                new Error("ActionContext.Null",
                "ActionContext is null."));
        }

        var modelState = actionContextAccessor.ActionContext.ModelState;

        if (request.patchDoc is null)
        {
            return Result.Failure(OrderErrors.NullPatchDoc);
        }

        var orderDetail = await orderDetailRepository
            .GetById(request.OrderDetailId);

        if (orderDetail is null)
        {
            return Result.Failure(OrderErrors.NotFoundOrderDetail);
        }

        if (modelState.IsValid)
        {
            request.patchDoc.ApplyTo(orderDetail, modelState);

            return Result.Success();
        }
        else
        {
            return Result.Failure
                (new Error("ModelState.Invalid",
                            "It was not possible to bind the incoming values from the request to the model correctly"));
        }
    }
}
