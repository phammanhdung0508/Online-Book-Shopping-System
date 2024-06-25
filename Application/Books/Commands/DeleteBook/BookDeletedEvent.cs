using MediatR;

namespace Application.Books.Commands.DeleteBook;

public sealed record BookDeletedEvent(Guid Id) : INotification;