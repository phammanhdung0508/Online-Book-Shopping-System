using Domain.Abstractions.IRepository;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class FeedbackRepository : IFeedbackRepository
{
    private readonly DbSet<Feedback> context;

    public FeedbackRepository(BookStoreDbContext context)
    {
        this.context = context.Set<Feedback>();
    }

    public void Create(Feedback feedback)
    {
        context.Add(feedback);
    }

    public void Delete(Guid Id)
    {
        var feedback = context.Find(Id);
        if (feedback is not null)
        {
            feedback.Remove();
            context.Update(feedback);
        }
    }

    public Task<List<Feedback>> Get(
        int pageIndex,
        int pageSize,
        string? filter,
        string? sort,
        string? sortBy,
        string? includeProperties,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Feedback?> GetById(
        Guid Id, 
        CancellationToken cancellationToken = default)
    {
        return await context
            .FirstOrDefaultAsync(f => f.Id == Id);
    }

    public void Update(Feedback feedback)
    {
        context.Update(feedback);
    }
}
