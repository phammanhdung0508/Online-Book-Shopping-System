using MediatR;

namespace Application.Feedbacks.Commands.DeleteFeedback;

public sealed record FeedbackDeletedEvent(Guid Id) : INotification;