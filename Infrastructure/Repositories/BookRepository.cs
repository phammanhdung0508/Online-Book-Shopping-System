using Domain.Abstractions.IRepository;
using Domain.Entities;
using Domain.Enum;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
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
        if(book is not null)
        {
            book.Remove();
            context.Update(book);
        }
    }

    public async Task<Book?> GetBookById
        (Guid Id, CancellationToken cancellationToken = default)
    {
        return await context.
            FirstOrDefaultAsync(b => b.Id == Id);
    }

    public async Task<List<Book>> GetBooks
        (Sort sort,
        string filter = "",
        string includeProperties = "",
        CancellationToken cancellationToken = default)
    {
        IQueryable<Book> query = context;

        if (string.IsNullOrEmpty(filter))
        {
            query.Where(b => b.Title.Contains(filter));
        }

        foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        switch (sort)
        {
            case Sort.NoSort:
                break;
            case Sort.Ascending:
                query.OrderBy(b => b.PublishedYear);
                break;
            case Sort.Descending:
                query.OrderByDescending(b => b.PublishedYear);
                break;
        }

        return await query.ToListAsync();
    }

    public void Update(Book book)
    {
        context.Update(book);
    }
}
