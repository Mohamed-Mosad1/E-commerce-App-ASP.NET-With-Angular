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
    internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(deliveryMethod => deliveryMethod.Cost).HasColumnType("decimal(18, 2)");
            builder.HasData(
                new DeliveryMethod() { Id = 1, ShortName = "DHL", Description = "Fastest Delivery time", Cost = 20 },
                new DeliveryMethod() { Id = 2, ShortName = "Aramex", Description = "Get it with 3 days", Cost = 10 },
                new DeliveryMethod() { Id = 3, ShortName = "Fedex", Description = "Slower but cheap", Cost = 5 },
                new DeliveryMethod() { Id = 4, ShortName = "Jumia", Description = "Free", Cost = 0 }
                );



        }
    }
}
