using Application.Abstractions.Messaging;

namespace Application.Feedbacks.Commands.UpdateFeedback;

public sealed record FeedbackUpdatedCommand(
    Guid Id,
    string Content,
    string FeedbackOn) : ICommand;