using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Abstractions.IRepository;
using Domain.Errors;
using Domain.Shared;

namespace Application.Users.Queries.GetAllUser;

internal sealed class GetAllUserQueryHandler
    : IQueryHandler<GetAllUserQuery, List<UserResponse>>
{
    private readonly IUserRepository userRepository;
    private readonly ICacheService cacheService;

    public GetAllUserQueryHandler
        (IUserRepository userRepository,
        ICacheService cacheService)
    {
        this.userRepository = userRepository;
        this.cacheService = cacheService;
    }

    public async Task<Result<List<UserResponse>>> Handle
        (GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var result = await cacheService.GetAsync<List<UserResponse>>(
                "users",
                async () =>
                {
                    var list = await userRepository.Get(cancellationToken);

                    if (list is not null)
                    {

                        var response = new List<UserResponse>();

                        foreach (var item in list)
                        {
                            response.Add
                                (new UserResponse(item.Id, item.Email, "", ""));
                        }

                        return response;
                    }

                    return null;
                }, cancellationToken);

        if (result is null)
        {
            return Result.Failure<List<UserResponse>>(Error.NullValue);
        }

        return result;
    }
}