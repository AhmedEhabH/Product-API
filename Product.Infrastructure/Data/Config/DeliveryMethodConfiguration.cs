using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Data.Config
{
    internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(x => x.Price).HasColumnType("decimal(18, 2)");
            builder.HasData(
                new DeliveryMethod { ID = 1, ShortName = "DHL", Description = "Fastest Delivery", DeliveryTime = "", Price = 50 },
                new DeliveryMethod { ID = 2, ShortName = "Aramex", Description = "Get It With 3 Days", DeliveryTime = "", Price = 30 },
                new DeliveryMethod { ID = 3, ShortName = "Fedex", Description = "Slower But Cheap", DeliveryTime = "", Price = 20 },
                new DeliveryMethod { ID = 4, ShortName = "Jumia", Description = "Free", DeliveryTime = "", Price = 0 }
                );
        }
    }
}
