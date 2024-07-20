using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Dto
{
    public class OrderItemDto
    {
        public int ProductItemId { get; set; }
        public string ProductItemName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}
