using Domain.Abstractions.IRepository;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly BookStoreDbContext context;

    public UserRepository(BookStoreDbContext context)
    {
        this.context = context;
    }

    public void Create(User user)
    {
        context.Users.Add(user);
    }

    public void Delete(Guid id)
    {
        var user = context.Users.Find(id);

        if (user is not null)
        {
            context.Users.Update(user);
        }
    }

    public async Task<List<User>> Get(CancellationToken cancellationToken = default)
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetUserByEmail
        (string email, CancellationToken cancellationToken = default)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}