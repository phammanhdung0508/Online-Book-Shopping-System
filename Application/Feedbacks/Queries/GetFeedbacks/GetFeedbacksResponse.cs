namespace Application.Feedbacks.Queries.GetFeedbacks;

public sealed record GetFeedbacksResponse(
    Guid Id,
    string Content,
    string FeedbackOn);