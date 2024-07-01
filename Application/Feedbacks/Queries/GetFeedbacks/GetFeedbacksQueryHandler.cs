using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Abstractions.IRepository;
using Domain.Errors;
using Domain.Shared;

namespace Application.Feedbacks.Queries.GetFeedbacks;

internal sealed class GetFeedbacksQueryHandler
    : IQueryHandler<GetFeedbacksQuery, List<GetFeedbacksResponse>>
{
    private readonly IFeedbackRepository feedbackRepository;
    private readonly ICacheService cacheService;

    public GetFeedbacksQueryHandler(
        IFeedbackRepository feedbackRepository,
        ICacheService cacheService)
    {
        this.feedbackRepository = feedbackRepository;
        this.cacheService = cacheService;
    }

    public async Task<Result<List<GetFeedbacksResponse>>> Handle(
        GetFeedbacksQuery request, CancellationToken cancellationToken)
    {
        var result = await cacheService.GetAsync<List<GetFeedbacksResponse>>(
            "feedbacks",
            async () =>
            {
                var list = await feedbackRepository.Get(
                    request.PageIndex,
                    request.PageSize,
                    request.Filter,
                    request.Sort,
                    request.SortBy,
                    request.IncludeProperties,
                    cancellationToken);

                if (list is not null)
                {
                    var response = new List<GetFeedbacksResponse>();

                    foreach (var item in list)
                    {
                        response.Add(
                            new GetFeedbacksResponse(
                                item.Id, 
                                item.Content,
                                item.FeedbackOn));
                    }

                    return response;
                }

                return null;
            }, cancellationToken);

        if (result is not null)
        {
            return result;
        }

        return Result.Failure<List<GetFeedbacksResponse>>(Error.NullValue);
    }
}
