using Application.Abstractions.Messaging;

namespace Application.Books.Commands.DeleteBook;

public sealed record DeleteBookCommand(
    string Id) : ICommand;