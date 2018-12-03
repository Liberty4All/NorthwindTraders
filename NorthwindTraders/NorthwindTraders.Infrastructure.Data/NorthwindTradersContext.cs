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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
