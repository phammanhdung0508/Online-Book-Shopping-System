using Application.Abstractions.Messaging;
using Domain.Abstractions.IRepository;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;
using MediatR;
using Domain.Errors;

namespace Application.Books.Commands.CreateBook;

public sealed class CreateBookCommandHandler 
    : ICommandHandler<CreateBookCommand>
{
    private readonly IBookRepository bookRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IPublisher publisher;

    public CreateBookCommandHandler(
        IBookRepository bookRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        this.bookRepository = bookRepository;
        this.unitOfWork = unitOfWork;
        this.publisher = publisher;
    }

    public async Task<Result> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        if(request.PublishedYear <= 0)
        {
            return Result.Failure(BookErrors.InvalidYear);
        }

        var book = new Book.Builder()
            .SetID(Guid.NewGuid())
            .SetTitle(request.Title)
            .SetAuthor(request.Author)
            .SetPublishedYear(request.PublishedYear)
            .SetPublisher(request.Publisher)
            .SetISBN(request.ISBN).Build();

        bookRepository.Create(book);

        await Task.WhenAll(
            unitOfWork.SaveChangeAsync(cancellationToken),
            publisher.Publish
                (new BookCreatedEvent (
                    book.Id, 
                    request.Title), cancellationToken));

        return Result.Success();
    }
}