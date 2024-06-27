using Domain.Entities;

namespace Domain.Abstractions.IRepository;

public interface IOrderRepository
{
    Task<List<Order>> Get(
        int pageIndex,
        int pageSize,
        string? filter,
        string? sort,
        string? sortBy,
        string? includeProperties,
        CancellationToken cancellationToken = default);
    Task<Order?> GetById(Guid id);
    void Create(Order order);
    void Update(Order order);
}