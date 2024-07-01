using Application.Abstractions.Messaging;

namespace Application.Feedbacks.Commands.DeleteFeedback;

public sealed record FeedbackDeletedCommand(Guid Id) : ICommand;