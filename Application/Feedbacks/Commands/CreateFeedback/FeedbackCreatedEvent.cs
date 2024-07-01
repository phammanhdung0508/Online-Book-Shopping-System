using MediatR;

namespace Application.Feedbacks.Commands.CreateFeedback;

public sealed record FeedbackCreatedEvent(
    Guid id) : INotification;