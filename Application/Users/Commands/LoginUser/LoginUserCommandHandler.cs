using Application.Abstractions.Messaging;
using Application.Abstractions.Services;
using Domain.Abstractions.IRepository;
using Domain.Errors;
using Domain.Shared;
using System.Text;
using System.Security.Cryptography;

namespace Application.Users.Commands.LoginUser;

internal sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly IUserRepository userRepository;
    private readonly IJWTTokenProvider jWTTokenProvider;

    public LoginUserCommandHandler
        (IUserRepository userRepository,
        IJWTTokenProvider jWTTokenProvider)
    {
        this.userRepository = userRepository;
        this.jWTTokenProvider = jWTTokenProvider;
    }

    public async Task<Result<LoginUserResponse>> Handle
        (LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmail(request.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<LoginUserResponse>(AuthenErrors.LoginFail);
        }

        if (!VerifyPasswordHash(request.Password, user.PasswordHash!, user.PasswordSalt!))
        {
            return Result.Failure<LoginUserResponse>(AuthenErrors.LoginFail);
        }

        var jwt = jWTTokenProvider.CreateToken(user);

        var response = new LoginUserResponse(
            user.Id,
            user.Email,
            user.Role!.Name,
            jwt);

        return response;
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computeHash.SequenceEqual(passwordHash);
        }
    }
}
