using Domain.Entities;

namespace Domain.Abstractions.IRepository;

public interface IFeedbackRepository
{
    Task<List<Feedback>> Get(
        int pageIndex,
        int pageSize,
        string? filter,
        string? sort,
        string? sortBy,
        string? includeProperties,
        CancellationToken cancellationToken = default);
    Task<Feedback?> GetById(Guid Id, CancellationToken cancellationToken = default);
    void Create(Feedback feedback);
    void Update(Feedback feedback);
    void Delete(Guid Id);
}