using Application.Abstractions.Messaging;

namespace Application.Feedbacks.Queries.GetFeedbacks;

public sealed record GetFeedbacksQuery(
    int PageIndex,
    int PageSize,
    string? Filter,
    string? Sort,
    string? SortBy,
    string? IncludeProperties) : IQuery<List<GetFeedbacksResponse>>;