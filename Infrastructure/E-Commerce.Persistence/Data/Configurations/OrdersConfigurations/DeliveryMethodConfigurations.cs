using E_Commerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.Configurations.OrdersConfigurations
{
    public class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(o => o.Price).HasColumnType("decimal(18,2)");
            builder.Property(o => o.ShortName).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(o => o.Description).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(o => o.DeliveryTime).HasColumnType("varchar").HasMaxLength(128);
        }
    }
}
