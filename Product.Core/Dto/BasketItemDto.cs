using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Dto
{
    public class BasketItemDto
    {
        public required int Id { get; set; }
        public required string ProductName { get; set; }
        public required string ProductPicture { get; set; }
        [Range(0.1, double.MaxValue, ErrorMessage = "Price Must be Greater Than Zero")]
        public required decimal Price { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Quantity Must be Greater Than Zero")]
        public required int Quantity { get; set; }
        public required string Category { get; set; }
    }
}
