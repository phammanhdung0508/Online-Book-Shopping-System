using Application.Abstractions.Messaging;

namespace Application.Feedbacks.Commands.CreateFeedback;

public sealed record FeedbackCreatedCommand(
    string Content,
    string FeedbackOn,
    Guid BookId,
    Guid UserId) : ICommand;