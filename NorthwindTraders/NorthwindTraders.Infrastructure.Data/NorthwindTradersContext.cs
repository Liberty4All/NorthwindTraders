using Microsoft.EntityFrameworkCore;
using NorthwindTraders.Core.Entity;
using System;

namespace NorthwindTraders.Infrastructure.Data
{
    public class NorthwindTradersContext : DbContext
    {
        public NorthwindTradersContext(DbContextOptions<NorthwindTradersContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders);

            modelBuilder.Entity<OrderDetail>()
            .HasKey(od => new { od.OrderID, od.ProductID });
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
    }
}
