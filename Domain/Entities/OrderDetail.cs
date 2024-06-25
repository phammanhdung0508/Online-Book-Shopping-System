using Domain.Primitives;

namespace Domain.Entities;

public sealed class OrderDetail : Entity
{
    public int Quantity { get; set; } = -1;
    public double UnitPrice { get; set; } = -1;

    /*One -------------------------------------------------*/
    public Guid BookId { get; private set; }
    public Book? Book { get; private set; }
    public Guid OrderId { get; private set; }
    public Order? Order { get; private set; }

    public OrderDetail() { }

    public class Builder
    {
        private OrderDetail _orderDetail = new OrderDetail();

        public Builder SetID(Guid id) { _orderDetail.Id = id; return this; }
        public Builder SetQuantity(int quantity) {  _orderDetail.Quantity = quantity; return this; }
        public Builder SetUnitPrice(double unitPrice) {  _orderDetail.UnitPrice = unitPrice; return this; }
        public Builder SetBookId(Guid bookId) { _orderDetail.BookId = bookId; return this; }
        public Builder SetOrderId(Guid orderId) { _orderDetail.OrderId = orderId; return this; }

        public OrderDetail Build() => _orderDetail;
    }
}
