using MediatR;

namespace Application.Feedbacks.Commands.UpdateFeedback;

public sealed record FeedbackUpdatedEvent(Guid Id) : INotification;