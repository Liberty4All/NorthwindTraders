using Microsoft.EntityFrameworkCore;
using NorthwindTraders.Core;
using NorthwindTraders.Core.DomainService;
using NorthwindTraders.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NorthwindTraders.Infrastructure.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly NorthwindTradersContext _context;

        public OrderRepository(NorthwindTradersContext context)
        {
            _context = context;
        }
        public Order Create(Order order)
        {
            if (order.Customer != null &&
                _context.ChangeTracker.Entries<Customer>()
                .FirstOrDefault(ce => ce.Entity.CustomerID == order.Customer.CustomerID) == null)
            {
                _context.Attach(order.Customer);
            }
            var customer = _context.Customers.Where(c => c.CustomerID == order.Customer.CustomerID).FirstOrDefault();
            if (customer is null)
            {
                throw new NotFoundException($"Customer ID: '{order.Customer.CustomerID}' cannot be found");
            }
            var saved = _context.Orders.Add(order).Entity;
            _context.SaveChanges();
            return saved;
        }

        public IEnumerable<Order> ReadAll()
        {
            return _context.Orders;
        }

        public Order ReadById(int orderId)
        {
            return _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefault(o => o.OrderId == orderId);
        }

        public Order Update(Order orderUpdate)
        {
            if (orderUpdate.Customer != null &&
                _context.ChangeTracker.Entries<Customer>()
                .FirstOrDefault(ce => ce.Entity.CustomerID == orderUpdate.Customer.CustomerID) == null)
            {
                _context.Attach(orderUpdate.Customer);
            }
            else
            {
                _context.Entry(orderUpdate).Reference(o => o.Customer).IsModified = true;
            }
            var updated = _context.Update(orderUpdate).Entity;
            _context.SaveChanges();
            return updated;
            //_context.Attach(orderUpdate).State = EntityState.Modified;
            //_context.Entry(orderUpdate).Reference(o => o.Customer).IsModified = true;
            //_context.SaveChanges();
            //return orderUpdate;
        }

        public Order Delete(int orderId)
        {
            var result = _context.Remove(new Order { OrderId = orderId }).Entity;
            _context.SaveChanges();
            return result;
        }
    }
}
