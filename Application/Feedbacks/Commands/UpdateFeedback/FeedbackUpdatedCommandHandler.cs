using Application.Abstractions.Messaging;
using Application.Abstractions.Caching;
using Domain.Abstractions.IRepository;
using Domain.Abstractions;
using Domain.Shared;
using Domain.Entities;
using Domain.Errors;
using MediatR;

namespace Application.Feedbacks.Commands.UpdateFeedback;

internal sealed class FeedbackUpdatedCommandHandler
    : ICommandHandler<FeedbackUpdatedCommand>
{
    private readonly IFeedbackRepository feedbackRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ICacheService cacheService;
    private readonly IPublisher publisher;

    public FeedbackUpdatedCommandHandler(
        IFeedbackRepository feedbackRepository,
        IUnitOfWork unitOfWork,
        ICacheService cacheService,
        IPublisher publisher)
    {
        this.feedbackRepository = feedbackRepository;
        this.unitOfWork = unitOfWork;
        this.cacheService = cacheService;
        this.publisher = publisher;
    }

    public async Task<Result> Handle(FeedbackUpdatedCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var result = await cacheService.GetAsync<Feedback>(
            "feedback",
            async () =>
            {
                var feedback = await feedbackRepository.GetById(id);

                if (feedback is not null)
                {
                    return feedback;
                }

                return null;
            }, cancellationToken);

        result.Update(request.Content, request.FeedbackOn);

        if (result is null)
        {
            return Result.Failure(FeedbackErrors.NotFound);
        }

        feedbackRepository.Update(result);

        await Task.WhenAll(
            unitOfWork.SaveChangeAsync(cancellationToken),
            publisher.Publish(
                new FeedbackUpdatedEvent(
                    id), cancellationToken));

        return Result.Success();
    }
}