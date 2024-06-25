using MediatR;

namespace Application.Books.Commands.CreateBook;

public sealed record BookCreatedEvent(
    Guid Id,
    string Title) : INotification;