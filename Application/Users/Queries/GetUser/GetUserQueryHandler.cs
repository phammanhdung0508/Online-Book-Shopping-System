using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Application.Abstractions.Services;
using Domain.Abstractions.IRepository;
using Domain.Errors;
using Domain.Shared;

namespace Application.Users.Queries.GetUser;

internal sealed class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserResponse>
{
    private readonly IUserRepository userRepository;
    private readonly ICacheService cacheService;
    private readonly IJWTTokenProvider jWTTokenProvider;

    public GetUserQueryHandler
        (IUserRepository userRepository,
        ICacheService cacheService,
        IJWTTokenProvider jWTTokenProvider)
    {
        this.userRepository = userRepository;
        this.cacheService = cacheService;
        this.jWTTokenProvider = jWTTokenProvider;
    }

    public async Task<Result<UserResponse>> Handle
        (GetUserQuery request, CancellationToken cancellationToken)
    {
        var result = await cacheService.GetAsync<UserResponse>(
                "users",
                async () =>
                {
                    var user = await userRepository.GetUserByEmail(request.Email, cancellationToken);

                    if (user is not null)
                    {
                        var jwt = jWTTokenProvider.CreateToken(user);

                        var response = new UserResponse(
                            user.Id, 
                            user.Email,
                            user.Role!.Name,
                            jwt);

                        return response;
                    }

                    return null;
                }, cancellationToken);

        if (result is null)
        {
            return Result.Failure<UserResponse>(Error.NullValue);
        }

        return result;
    }
}
