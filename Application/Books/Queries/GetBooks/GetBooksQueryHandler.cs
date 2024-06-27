using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Abstractions.IRepository;
using Domain.Errors;
using Domain.Shared;

namespace Application.Books.Queries.GetBooks;

internal sealed class GetBooksQueryHandler
    : IQueryHandler<GetBooksQuery, List<GetBooksResponse>>
{
    private readonly IBookRepository bookRepository;
    private readonly ICacheService cacheService;

    public GetBooksQueryHandler(
        IBookRepository bookRepository,
        ICacheService cacheService)
    {
        this.bookRepository = bookRepository;
        this.cacheService = cacheService;
    }

    public async Task<Result<List<GetBooksResponse>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var result = await cacheService.GetAsync<List<GetBooksResponse>>(
            "books",
            async () =>
            {
                var list = await bookRepository.Get(
                    request.PageIndex,
                    request.PageSize,
                    request.Filter,
                    request.Sort,
                    request.SortBy,
                    request.IncludeProperties,
                    cancellationToken);

                if (list is not null)
                {
                    var response = new List<GetBooksResponse>();

                    foreach (var item in list)
                    {
                        response.Add(new GetBooksResponse(item.Id, item.Title));
                    }

                    return response;
                }

                return null;
            }, cancellationToken);

        if (result is not null) {
            return result;
        }

        return Result.Failure<List<GetBooksResponse>>(Error.NullValue);
    }
}
