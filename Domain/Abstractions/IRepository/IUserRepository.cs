using Domain.Entities;

namespace Domain.Abstractions.IRepository;

public interface IUserRepository
{
    Task<List<User>> Get(CancellationToken cancellationToken = default);
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken = default);
    Task<User?> GetById(Guid id, CancellationToken cancellationToken = default);
    void Create(User user);
    void Delete(Guid id);
}