using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Entities.OrderAggregate
{
    public class Order : BaseEntity<int>
    {
        public Order()
        {
            
        }

        public Order(string buyerEmail, ShippingAddress shippingAddress, DeliveryMethod deliveryMethods, IReadOnlyList<OrderItem> orderItems, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethods = deliveryMethods;
            OrderItems = orderItems;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public ShippingAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethods { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public decimal GetTotal() => SubTotal + DeliveryMethods.Cost;
    }
}
