using MediatR;

namespace Application.Books.Commands.UpdateBook;

internal sealed record BookUpdatedEvent(
    Guid Id,
    string Title
    ) : INotification;