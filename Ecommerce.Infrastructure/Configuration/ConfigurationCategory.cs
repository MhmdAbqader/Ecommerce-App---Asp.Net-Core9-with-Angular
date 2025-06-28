using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configuration
{
    public class ConfigurationCategory : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            
            builder.Property(a => a.Id).IsRequired(true);

            //[Required]
            //[MaxLength(20)]
            builder.Property(a => a.Name).IsRequired(true).HasMaxLength(20);
            
        }
    }
}
