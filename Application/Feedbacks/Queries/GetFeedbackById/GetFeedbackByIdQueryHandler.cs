using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Abstractions.IRepository;
using Domain.Errors;
using Domain.Shared;

namespace Application.Feedbacks.Queries.GetFeedbackById;

internal sealed class GetFeedbackByIdQueryHandler
    : IQueryHandler<GetFeedbackByIdQuery, GetFeedbackByIdResponse>
{
    private readonly IFeedbackRepository feedbackRepository;
    private readonly ICacheService cacheService;

    public GetFeedbackByIdQueryHandler(
        IFeedbackRepository feedbackRepository,
        ICacheService cacheService)
    {
        this.feedbackRepository = feedbackRepository;
        this.cacheService = cacheService;
    }

    public async Task<Result<GetFeedbackByIdResponse>> Handle(
        GetFeedbackByIdQuery request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var result = await cacheService.GetAsync<GetFeedbackByIdResponse>(
            $"feedbacks-{id}",
            async () =>
            {
                var feedback = await feedbackRepository.GetById(id);

                if (feedback is not null)
                {
                    var response = new GetFeedbackByIdResponse(
                        feedback.Id,
                        feedback.Content,
                        feedback.FeedbackOn,
                        feedback.FeedbackAt.ToString(),
                        feedback.Status);

                    return response;
                }

                return null;
            }, cancellationToken);

        if (result is not null)
        {
            return result;
        }

        return Result.Failure<GetFeedbackByIdResponse>(Error.NullValue);
    }
}
