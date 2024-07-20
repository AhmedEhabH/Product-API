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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            /*builder.HasOne(o => o.DeliveryMethod)
                .WithMany(d => d.Orders)
                .HasForeignKey(o => o.DeliveryMethodId)
                .IsRequired();*/

            builder.OwnsOne(x => x.ShipToAddress, n =>
            {
                n.WithOwner();
            });
            builder.Property(x => x.OrderStatus).HasConversion(o => o.ToString(), o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o));
            builder.HasMany(x => x.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.SubTotal).HasColumnType("decimal(18, 2)");
        }
    }
}
