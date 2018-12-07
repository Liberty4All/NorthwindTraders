using Microsoft.EntityFrameworkCore;
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
            var updated = _context.Update(orderUpdate).Entity;
            _context.SaveChanges();
            return updated;
        }

        public Order Delete(int orderId)
        {
            var result = _context.Remove(new Order { OrderId = orderId }).Entity;
            _context.SaveChanges();
            return result;
        }
    }
}
