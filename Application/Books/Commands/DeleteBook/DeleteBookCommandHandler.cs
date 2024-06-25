using Application.Abstractions.Messaging;
using Domain.Abstractions.IRepository;
using Domain.Abstractions;
using Domain.Shared;
using MediatR;

namespace Application.Books.Commands.DeleteBook;

public class DeleteBookCommandHandler
    : ICommandHandler<DeleteBookCommand>
{
    private readonly IBookRepository bookRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IPublisher publisher;

    public DeleteBookCommandHandler(
        IBookRepository bookRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        this.bookRepository = bookRepository;
        this.unitOfWork = unitOfWork;
        this.publisher = publisher;
    }

    public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id);

        bookRepository.Delete(id);

        await Task.WhenAll(
            unitOfWork.SaveChangeAsync(cancellationToken),
            publisher.Publish(new BookDeletedEvent(id), cancellationToken));

        return Result.Success();
    }
}
