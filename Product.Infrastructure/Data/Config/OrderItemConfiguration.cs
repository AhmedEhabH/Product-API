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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            //builder.HasKey(x => x.OrderItemsID);
            builder.OwnsOne(x => x.ProductItemOrder, n => { n.WithOwner(); });
            builder.Property(x => x.Price).HasColumnType("decimal(18, 2)");
        }
    }
}
