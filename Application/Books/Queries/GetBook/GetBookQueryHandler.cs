using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Application.Users.Queries;
using Domain.Abstractions.IRepository;
using Domain.Errors;
using Domain.Shared;

namespace Application.Books.Queries.GetBook;

internal sealed class GetBookQueryHandler
    : IQueryHandler<GetBookQuery, List<GetBookResponse>>
{
    private readonly IBookRepository bookRepository;
    private readonly ICacheService cacheService;

    public GetBookQueryHandler(
        IBookRepository bookRepository,
        ICacheService cacheService)
    {
        this.bookRepository = bookRepository;
        this.cacheService = cacheService;
    }

    public async Task<Result<List<GetBookResponse>>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        var result = await cacheService.GetAsync<List<GetBookResponse>>(
            "books",
            async () =>
            {
                var list = await bookRepository.GetBooks(
                    request.sort,
                    request.Filter,
                    request.includeProperties,
                    cancellationToken);

                if (list is not null)
                {
                    var response = new List<GetBookResponse>();
                    foreach (var item in list)
                    {
                        response.Add(new GetBookResponse(item.Id, item.Title));
                    }
                    return response;
                }

                return null;
            }, cancellationToken);

        if (result is not null) {
            return result;
        }

        return Result.Failure<List<GetBookResponse>>(Error.NullValue);
    }
}
