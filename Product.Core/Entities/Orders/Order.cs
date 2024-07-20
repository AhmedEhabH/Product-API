using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Entities.Orders
{
    public class Order : BaseEntity<int>
    {
        public Order()
        {

        }

        public Order(
            string buyerEmail, ShipAddress shipToAddress, DeliveryMethod deliveryMethod,
            IReadOnlyList<OrderItem> orderItems, decimal subTotal, string paymentIntentId = null
            )
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public ShipAddress ShipToAddress { get; set; }
        //public int DeliveryMethodId { get; set; } // Foreign key
        public DeliveryMethod DeliveryMethod { get; set; } // Navigation property
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public string? PaymentIntentId { get; set; } = null;
        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Price;
        }
    }
}
