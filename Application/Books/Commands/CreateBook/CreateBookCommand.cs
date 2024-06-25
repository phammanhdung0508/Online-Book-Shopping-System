using Application.Abstractions.Messaging;

namespace Application.Books.Commands.CreateBook;

public sealed record CreateBookCommand
    (string Title,
    string Author,
    int PublishedYear,
    string Publisher,
    string ISBN) : ICommand;