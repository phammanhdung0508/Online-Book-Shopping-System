using Application.Abstractions.Messaging;
using Domain.Abstractions.IRepository;
using Domain.Abstractions;
using MediatR;
using Domain.Shared;

namespace Application.Feedbacks.Commands.DeleteFeedback;

internal sealed class FeedbackDeletedCommandHandler
    : ICommandHandler<FeedbackDeletedCommand>
{
    private readonly IFeedbackRepository feedbackRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IPublisher publisher;

    public FeedbackDeletedCommandHandler(
        IFeedbackRepository feedbackRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        this.feedbackRepository = feedbackRepository;
        this.unitOfWork = unitOfWork;
        this.publisher = publisher;
    }

    public async Task<Result> Handle(FeedbackDeletedCommand request, CancellationToken cancellationToken)
    {
        feedbackRepository.Delete(request.Id);

        await Task.WhenAll(
            unitOfWork.SaveChangeAsync(cancellationToken),
            publisher.Publish(new FeedbackDeletedEvent(request.Id), cancellationToken));

        return Result.Success();
    }
}