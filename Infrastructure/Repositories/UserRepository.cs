using Domain.Abstractions.IRepository;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly DbSet<User> context;

    public UserRepository(BookStoreDbContext context)
    {
        this.context = context.Set<User>();
    }

    public void Create(User user)
    {
        context.Add(user);
    }

    public void Delete(Guid id)
    {
        var user = context.Find(id);

        if (user is not null)
        {
            context.Update(user);
        }
    }

    public async Task<List<User>> Get
        (CancellationToken cancellationToken = default)
    {
        return await context.ToListAsync();
    }

    public async Task<User?> GetByEmail
        (string email, CancellationToken cancellationToken = default)
    {
        return await context
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetById
        (Guid id, CancellationToken cancellationToken = default)
    {
        return await context
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}