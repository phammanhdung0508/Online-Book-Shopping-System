using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Abstractions.IRepository;
using Domain.Entities;
using Domain.Shared;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler
    : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IPublisher publisher;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
        this.publisher = publisher;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        CreatePasswordHash(
            request.password,
            out byte[] password_hash,
            out byte[] password_salt);

        var user = new User.Builder()
            .SetID(Guid.NewGuid())
            .SetEmail(request.email)
            .SetPasswordHash(password_hash)
            .SetPasswordSalt(password_salt).Build();

        userRepository.Create(user);

        await Task.WhenAll(
            unitOfWork.SaveChangeAsync(cancellationToken),
            publisher.Publish
                (new UserCreatedEvent { Id = user.Id, Email = user.Email }, cancellationToken));

        return Result.Success();
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}