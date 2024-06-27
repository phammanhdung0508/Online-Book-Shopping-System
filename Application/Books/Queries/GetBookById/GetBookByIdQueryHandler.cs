using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Application.Books.Queries.GetAllBook;
using Domain.Abstractions.IRepository;
using Domain.Errors;
using Domain.Shared;

namespace Application.Books.Queries.GetBookById;

public sealed class GetBookByIdQueryHandler 
    : IQueryHandler<GetBookByIdQuery, GetBookByIdResponse>
{
    private readonly IBookRepository bookRepository;
    private readonly ICacheService cacheService;

    public GetBookByIdQueryHandler(
        IBookRepository bookRepository,
        ICacheService cacheService)
    {
        this.bookRepository = bookRepository;
        this.cacheService = cacheService;
    }

    public async Task<Result<GetBookByIdResponse>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id);

        var result = await cacheService.GetAsync<GetBookByIdResponse>(
            $"books-{id}",
            async () =>
            {
                var book = await bookRepository.GetById(id);

                if (book is not null)
                {
                    var response = new GetBookByIdResponse(
                        book.Id,
                        book.Title);

                    return response;
                }

                return null;
            }, cancellationToken);

        if (result is not null)
        {
            return result;
        }

        return Result.Failure<GetBookByIdResponse>(Error.NullValue);
    }
}
