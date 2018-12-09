using Microsoft.EntityFrameworkCore;
using NorthwindTraders.Core.DomainService;
using NorthwindTraders.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NorthwindTraders.Infrastructure.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly NorthwindTradersContext _context;

        public CustomerRepository(NorthwindTradersContext context)
        {
            _context = context;
        }

        public Customer Create(Customer customer)
        {
            var result = _context.Customers.Add(customer).Entity;
            _context.SaveChanges();
            return result;
        }

        public IEnumerable<Customer> ReadAll()
        {
            return _context.Customers;
        }

        public Customer ReadById(string customerId)
        {
            return _context.Customers
                .FirstOrDefault(c => c.CustomerID == customerId);
        }

        public Customer ReadByIdIncludeOrders(string customerId)
        {
            return _context.Customers
                .Include(c => c.Orders)
                .FirstOrDefault(c => c.CustomerID == customerId);

        }

        public Customer Update(Customer customerUpdate)
        {
    //        if (orderUpdate.Customer != null &&
    //_context.ChangeTracker.Entries<Customer>()
    //.FirstOrDefault(ce => ce.Entity.CustomerID == orderUpdate.Customer.CustomerID) == null)
    //        {
    //            _context.Attach(orderUpdate.Customer);
    //        }
            var updated = _context.Update(customerUpdate).Entity;
            _context.SaveChanges();
            return updated;

        }

        public Customer Delete(string customerId)
        {
            var custRemoved = _context.Remove(new Customer { CustomerID = customerId }).Entity;
            _context.SaveChanges();
            return custRemoved;
        }
    }
}
