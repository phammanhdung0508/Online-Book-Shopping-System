using Application.Abstractions.Messaging;

namespace Application.Feedbacks.Queries.GetFeedbackById;

public sealed record GetFeedbackByIdQuery(Guid Id) : IQuery<GetFeedbackByIdResponse>;