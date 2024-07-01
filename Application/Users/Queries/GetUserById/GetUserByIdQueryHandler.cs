using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Abstractions.IRepository;
using Domain.Errors;
using Domain.Shared;

namespace Application.Users.Queries.GetUserById;

internal sealed class GetUserByIdQueryHandler
    : IQueryHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    private readonly IUserRepository userRepository;
    private readonly ICacheService cacheService;

    public GetUserByIdQueryHandler
        (IUserRepository userRepository,
        ICacheService cacheService)
    {
        this.userRepository = userRepository;
        this.cacheService = cacheService;
    }

    public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await cacheService.GetAsync<GetUserByIdResponse>(
            $"users-{request.Id}",
            async () =>
            {
                var user = await userRepository.GetById(request.Id);

                if (user is not null)
                {
                    var response = new GetUserByIdResponse(
                        user.Id,
                        user.Email,
                        user.Phone,
                        user.Role!.Name);

                    return response;
                }

                return null;
            }, cancellationToken);

        if (result is not null)
        {
            return result;
        }

        return Result.Failure<GetUserByIdResponse>(Error.NullValue);
    }
}
