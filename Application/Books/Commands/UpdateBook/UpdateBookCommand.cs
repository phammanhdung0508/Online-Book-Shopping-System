using Application.Abstractions.Messaging;

namespace Application.Books.Commands.UpdateBook;

public sealed record UpdateBookCommand(
    string id,
    string title,
    string author) : ICommand;