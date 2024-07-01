using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Abstractions.IRepository;
using Domain.Errors;
using Domain.Shared;

namespace Application.Users.Queries.GetUsers;

internal sealed class GetUsersQueryHandler
    : IQueryHandler<GetUsersQuery, List<GetUsersResponse>>
{
    private readonly IUserRepository userRepository;
    private readonly ICacheService cacheService;

    public GetUsersQueryHandler
        (IUserRepository userRepository,
        ICacheService cacheService)
    {
        this.userRepository = userRepository;
        this.cacheService = cacheService;
    }

    public async Task<Result<List<GetUsersResponse>>> Handle
        (GetUsersQuery request, CancellationToken cancellationToken)
    {
        var result = await cacheService.GetAsync<List<GetUsersResponse>>(
                "users",
                async () =>
                {
                    var list = await userRepository.Get(cancellationToken);

                    if (list is not null)
                    {

                        var response = new List<GetUsersResponse>();

                        foreach (var item in list)
                        {
                            response.Add
                                (new GetUsersResponse(item.Id, item.Email, "", ""));
                        }

                        return response;
                    }

                    return null;
                }, cancellationToken);

        if (result is null)
        {
            return Result.Failure<List<GetUsersResponse>>(Error.NullValue);
        }

        return result;
    }
}