using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Models;
using Ecommerce.Core.Models.OrderOperations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>        //DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                        
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<DeliveryMethod>DeliveryMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(new Category {Name = "c1",Id=1 , Description="first category" });
            
            modelBuilder.Entity<Product>().HasData(
                new Product {Name = "p1",Id=1 , Description="first product",Price=90,CategoryId=1 , ImgURL = "http://"},
                new Product {Name = "p2",Id=2 , Description= "second product", Price=90,CategoryId=1, ImgURL = "http://" }
                );
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // order Operation 
         //   modelBuilder.Entity<ProductItemOrderd>().HasNoKey();
          //   modelBuilder.Entity<ShipToAddress>().HasNoKey();
        }


        }
    }
