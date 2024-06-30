using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Abstractions.IRepository;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using MediatR;

namespace Application.Feedbacks.Commands.CreateFeedback;

internal sealed class FeedbackCreatedCommandHandler
    : ICommandHandler<FeedbackCreatedCommand>
{
    private readonly IFeedbackRepository feedbackRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IPublisher publisher;

    public FeedbackCreatedCommandHandler(
        IFeedbackRepository feedbackRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        this.feedbackRepository = feedbackRepository;
        this.unitOfWork = unitOfWork;
        this.publisher = publisher;
    }

    public async Task<Result> Handle
        (FeedbackCreatedCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Content))
        {
            return Result.Failure(FeedbackErrors.ContentIsEmptyOrNull);
        }

        if (string.IsNullOrEmpty(request.FeedbackOn))
        {
            return Result.Failure(FeedbackErrors.FeedbackOnIsEmptyOrNull);
        }

        var feedback = new Feedback.Builder()
            .SetID(Guid.NewGuid())
            .SetContent(request.Content)
            .SetFeedbackOn(request.FeedbackOn)
            .SetFeedbackAt(DateTime.UtcNow)
            .SetStatus("Pending")
            .SetBookId(request.BookId)
            .SetUserId(request.UserId)
            .Build();

        feedbackRepository.Create(feedback);

        await Task.WhenAll(
            unitOfWork.SaveChangeAsync(cancellationToken),
            publisher.Publish(new FeedbackCreatedEvent(feedback.Id), cancellationToken));

        return Result.Success();
    }
}
