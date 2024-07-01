using Domain.Entities;

namespace Domain.Abstractions.IRepository;

public interface IOrderDetailRepository
{
    List<OrderDetail> GetByOrderId(Guid orderId);
    Task<OrderDetail?> GetById(Guid id);
    void Create(List<OrderDetail> orderDetails);
    void Update(OrderDetail orderDetail);
}
