using Product.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Dto
{
    public class OrderToReturnDto
    {
        public int OrderId { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public ShipAddress ShipToAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string OrderStatus { get; set; }
    }
}
