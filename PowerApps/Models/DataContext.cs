using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PowerApps.Models
{
    public class DataContext : DbContext
    {
        public IConfiguration configuration { get; }

        public DataContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<Agent> Agents { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));

            base.OnConfiguring(optionsBuilder);
        }
    }
}
