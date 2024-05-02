using E_commerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Infrastructure.Data.Config
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Id).IsRequired();
            builder.Property(P => P.Name).HasMaxLength(30);
            builder.Property(P => P.Price).HasColumnType("decimal(18, 2)");

            // Seed Data
            builder.HasData(
                new Product { Id = 1, Name = "Product One",   Description = "P1", Price = 1000, CategoryId = 1, ProductPicture = "https://" },
                new Product { Id = 2, Name = "Product Two",   Description = "P2", Price = 2000, CategoryId = 2, ProductPicture = "https://" },
                new Product { Id = 3, Name = "Product Three", Description = "P3", Price = 3000, CategoryId = 3, ProductPicture = "https://" }
            );

        }
    }
}
