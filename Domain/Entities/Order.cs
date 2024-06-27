using Domain.Primitives;

namespace Domain.Entities;

public sealed class Order : Entity
{
    public DateTime OrderDate { get; private set; } = DateTime.MinValue;
    public bool IsRemoved { get; private set; } = false;
    public DateTime RemovedAt { get; private set; } = DateTime.MinValue;

    /*One -------------------------------------------------*/
    public Guid UserId { get; private set; }
    public User? User { get; private set; }
    /*Many -------------------------------------------------*/
    public ICollection<OrderDetail>? OrderDetails { get; private set; }

    public Order() { }

    public class Builder
    {
        private Order _order = new Order();

        public Builder SetID(Guid id) { _order.Id = id; return this; }
        public Builder SetOrderDate(DateTime orderDate) { _order.OrderDate = orderDate; return this; }
        public Builder SetIsRemoved(bool isRemoved) { _order.IsRemoved = isRemoved; return this; }
        public Builder SetRemovedAt(DateTime removedAt) { _order.RemovedAt = removedAt; return this; }
        public Builder SetUserId(Guid userId) { _order.UserId = userId; return this; }
        //public Builder SetOrderDetails(List<OrderDetail> list) { _order.OrderDetails = list; return this; }

        public Order Build() => _order;
    }
}
