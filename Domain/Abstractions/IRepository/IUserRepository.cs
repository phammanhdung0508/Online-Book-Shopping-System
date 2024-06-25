using Domain.Entities;

namespace Domain.Abstractions.IRepository;

public interface IUserRepository
{
    Task<List<User>> Get(CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken = default);
    void Create(User user);
    void Delete(Guid id);
}