using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                        
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(new Category {Name = "c1",Id=1 , Description="first category" });
            
            modelBuilder.Entity<Product>().HasData(
                new Product {Name = "p1",Id=1 , Description="first product",Price=90,CategoryId=1 },
                new Product {Name = "p2",Id=2 , Description= "second product", Price=90,CategoryId=1 }
                );
        }


        }
    }
