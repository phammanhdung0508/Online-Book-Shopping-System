using Domain.Abstractions.IRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class OrderDetailRepository 
    : IOrderDetailRepository
{
    private readonly DbSet<OrderDetail> context;
    public OrderDetailRepository(DbSet<OrderDetail> context)
    {
        this.context = context;
    }

    public void Create(List<OrderDetail> orderDetails)
    {
        context.AddRange(orderDetails);
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
