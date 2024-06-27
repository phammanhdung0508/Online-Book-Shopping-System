using Domain.Abstractions.IRepository;
using Domain.Entities;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class OrderRepository
    : IOrderRepository
{
    private readonly DbSet<Order> context;
    public OrderRepository(DbSet<Order> context)
    {
        this.context = context;
    }

    public void Create(Order order)
    {
        context.Add(order);
    }

    public async Task<List<Order>> Get(
        int pageIndex, 
        int pageSize, 
        string? filter, 
        string? sort, 
        string? sortBy, 
        string? includeProperties, 
        CancellationToken cancellationToken = default)
    {
        IQueryable<Order> query = context;

        DateTime.TryParse(filter, out var date);    // Maybe it will bug right here.

        if (!string.IsNullOrEmpty(filter))
        {
            query.Where(b => b.OrderDate < date);
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

        return await Pagination<Order>.Get(query, pageIndex, pageSize);
    }

    public async Task<Order?> GetById(Guid id)
    {
        return await context.FindAsync(id);
    }

    public void Update(Order order)
    {
        context.Update(order);
    }
}
