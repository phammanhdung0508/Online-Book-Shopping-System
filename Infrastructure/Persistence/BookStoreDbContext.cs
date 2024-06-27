using Domain.Entities;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class BookStoreDbContext : DbContext
{
    public DbSet<Book>? Books { get; set; }
    public DbSet<Order>? Orders { get; set; }
    public DbSet<OrderDetail>? OrderDetails { get; set; }
    public DbSet<Role>? Roles { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<Feedback>? Feedbacks { get; set; }

    public BookStoreDbContext
        (DbContextOptions<BookStoreDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SetPrimaryKey(modelBuilder);
    }

    private void SetPrimaryKey(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(b => b.HasKey(u => u.Id));
        modelBuilder.Entity<Order>(b => b.HasKey(u => u.Id));
        modelBuilder.Entity<OrderDetail>(b => b.HasKey(u => u.Id));
        modelBuilder.Entity<Role>(b => b.HasKey(u => u.Id));
        modelBuilder.Entity<User>(b => b.HasKey(u => u.Id));
        modelBuilder.Entity<Feedback>(b => b.HasKey(u => u.Id));
    }
}
