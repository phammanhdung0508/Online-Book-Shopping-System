using Domain.Abstractions.IRepository;
using Domain.Entities;
using Domain.Shared;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class BookRepository : IBookRepository
{
    private readonly DbSet<Book> context;

    public BookRepository(BookStoreDbContext context)
    {
        this.context = context.Set<Book>();
    }

    public void Create(Book book)
    {
        context.Add(book);
    }

    public void Delete(Guid Id)
    {
        var book = context.Find(Id);
        if (book is not null)
        {
            book.Remove();
            context.Update(book);
        }
    }

    public async Task<Book?> GetById
        (Guid Id, CancellationToken cancellationToken = default)
    {
        return await context.
            FirstOrDefaultAsync(b => b.Id == Id);
    }

    public async Task<List<Book>> Get(
        int pageIndex,
        int pageSize,
        string? filter,
        string? sort,
        string? sortBy,
        string? includeProperties,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Book> query = context;

        if (!string.IsNullOrEmpty(filter))
        {
            query.Where(b => b.Title.Contains(filter));
        }

        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        if (!string.IsNullOrEmpty(sort) &&
            !string.IsNullOrEmpty(sortBy))
        {
            switch (sort)
            {
                case "asc":
                    query.OrderBy(b => EF.Property<object>(b, sortBy));
                    break;
                case "desc":
                    query.OrderByDescending(b => EF.Property<object>(b, sortBy));
                    break;
                default: break;
            }
        }

        return await Pagination<Book>.Get(query, pageIndex, pageSize);
    }

    public void Update(Book book)
    {
        context.Update(book);
    }
}
