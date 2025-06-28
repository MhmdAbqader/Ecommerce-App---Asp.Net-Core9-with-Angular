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
    public class ConfigurationOrderLine : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.OwnsOne(a => a.ProductItemOrderd, a => { a.WithOwner(); });
            builder.Property(pric => pric.Price).HasColumnType("decimal(18,3)");
            //throw new NotImplementedException();
        }
    }
}
