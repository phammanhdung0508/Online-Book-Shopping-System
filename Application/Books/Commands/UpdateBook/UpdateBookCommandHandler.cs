using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Abstractions.IRepository;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using MediatR;

namespace Application.Books.Commands.UpdateBook;

internal class UpdateBookCommandHandler : ICommandHandler<UpdateBookCommand>
{
    private readonly IBookRepository bookRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IPublisher publisher;
    private readonly ICacheService cacheService;

    public UpdateBookCommandHandler(
        IBookRepository bookRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher,
        ICacheService cacheService)
    {
        this.bookRepository = bookRepository;
        this.unitOfWork = unitOfWork;
        this.publisher = publisher;
        this.cacheService = cacheService;
    }

    public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.id);

        var result = await cacheService.GetAsync<Book>(
            "users",
            async () =>
            {
                var book = await bookRepository.GetById(id);

                if (book is not null)
                {
                    return book;
                }

                return null;
            }, cancellationToken);

        result.Update(request.title, request.author);

        if(result is null)
        {
            return Result.Failure(BookErrors.NotFound);
        }

        bookRepository.Update(result);

        await Task.WhenAll(
            unitOfWork.SaveChangeAsync(cancellationToken),
            publisher.Publish(
                new BookUpdatedEvent(
                    result.Id,
                    request.title
            ),cancellationToken));

        return Result.Success();
    }
}
