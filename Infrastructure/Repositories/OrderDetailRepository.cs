using Domain.Abstractions.IRepository;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class OrderDetailRepository 
    : IOrderDetailRepository
{
    private readonly DbSet<OrderDetail> context;
    public OrderDetailRepository(BookStoreDbContext context)
    {
        this.context = context.Set<OrderDetail>();
    }

    public void Create(List<OrderDetail> orderDetails)
    {
        context.AddRange(orderDetails);
    }

    public async Task<OrderDetail?> GetById(Guid id)
    {
        return await context.FirstOrDefaultAsync(od => od.Id == id);
    }

    public List<OrderDetail> GetByOrderId(Guid orderId)
    {
        return context
            .Where(od => od.OrderId == orderId)
            .ToList();
    }

    public void Update(OrderDetail orderDetail)
    {
        context.Update(orderDetail);
    }
}
