using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Models.OrderOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configuration
{
    public class ConfigurationOrder : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(add => add.ShipToAddress,u => { u.WithOwner(); });
            builder.Property(p => p.OrderStatusss).HasConversion(
                //x => ((int)x), x => (OrderStatus)Enum.Parse(typeof(OrderStatus),x.ToString())//save value as int in Db
                z => z.ToString(), z => (OrderStatus)Enum.Parse(typeof(OrderStatus),z) //save value as string in Db
                );
            builder.HasMany(ordLines => ordLines.OrderLines).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(a => a.CustomerEmail).IsRequired(true); 

        }
    }
}
