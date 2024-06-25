namespace Domain.Abstractions;

public interface IUnitOfWork
{
    Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
    void Dispose();
}