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
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(C => C.Id).IsRequired();
            builder.Property(C => C.Name).HasMaxLength(30);

            // Seed Data
            builder.HasData(
                new Category { Id = 1, Name = "Category One", Description = "1" },
                new Category { Id = 2, Name = "Category Two", Description = "2" },
                new Category { Id = 3, Name = "Category Three", Description = "3" }
            );


        }
    }
}
