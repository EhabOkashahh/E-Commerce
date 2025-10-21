using E_Commerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(P => P.Description).HasColumnType("varchar").HasMaxLength(512);
            builder.Property(P => P.PictureUrl).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(P => P.Price).HasColumnType("decimal(18,2)").HasMaxLength(256);

            builder.HasOne(p => p.Brand).WithMany().HasForeignKey(P=>P.BrandId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.Type).WithMany().HasForeignKey(P=>P.TypeId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
