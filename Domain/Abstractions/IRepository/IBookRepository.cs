using Domain.Entities;
using Domain.Enum;

namespace Domain.Abstractions.IRepository;

public interface IBookRepository
{
    Task<List<Book>> GetBooks(
        Sort sort,
        string filter = "",
        string includeProperties = "",
        CancellationToken cancellationToken = default);
    Task<Book?> GetBookById(Guid Id, CancellationToken cancellationToken = default);
    void Create(Book book);
    void Update(Book book);
    void Delete(Guid Id);
}