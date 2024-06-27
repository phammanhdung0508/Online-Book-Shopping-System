using Domain.Entities;

namespace Domain.Abstractions.IRepository;

public interface IOrderDetailRepository
{
    List<OrderDetail> GetByOrderId(Guid orderId);
    void Create(List<OrderDetail> orderDetails);
    void Update(OrderDetail orderDetail);
}
