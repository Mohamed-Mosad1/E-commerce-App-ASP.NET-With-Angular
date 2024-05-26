using E_commerce.Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Infrastructure.Data.Config.OrderConfig
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, shippingAddress => shippingAddress.WithOwner());
            builder.Property(o => o.OrderStatus)
                .HasConversion(o=>o.ToString(), orderStatus => (OrderStatus) Enum.Parse(typeof(OrderStatus), orderStatus));

            builder.HasMany(o=>o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18, 2)");


        }
    }
}
