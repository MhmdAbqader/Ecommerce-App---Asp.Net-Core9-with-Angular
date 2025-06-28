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
    public class ConfigurationDeliveryMethod : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(p => p.DeliveryPrice).HasColumnType("decimal(18,1)");
            builder.HasData(
                    new DeliveryMethod { Id = 1, DeliveryMethodShortName="Global Delivery UK",Description="Fast Time", DeliveryPrice=99.999M },
                    new DeliveryMethod { Id = 2, DeliveryMethodShortName = "DHL", Description="Delivery in 3 Days", DeliveryPrice=50.55M },
                    new DeliveryMethod { Id = 3, DeliveryMethodShortName ="Armex",Description="Free", DeliveryPrice=0.00M }
                );
        }
    }
}
