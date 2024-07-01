namespace Application.Feedbacks.Queries.GetFeedbackById;

public sealed record GetFeedbackByIdResponse(
    Guid Id,
    string Content,
    string FeedbackOn,
    string FeedbackAt,
    string Status);