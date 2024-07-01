using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Abstractions.IRepository;
using Domain.Shared;
using MediatR;

namespace Application.Users.Commands.DeleteUser;

public sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IPublisher publisher;

    public DeleteUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
        this.publisher = publisher;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        userRepository.Delete(request.userId);

        await Task.WhenAll(
            unitOfWork.SaveChangeAsync(cancellationToken),
            publisher.Publish(new UserDeletedEvent { Id = request.userId }, cancellationToken));

        return Result.Success();
    }
}