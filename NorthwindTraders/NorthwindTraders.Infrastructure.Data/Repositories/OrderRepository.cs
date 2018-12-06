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
            var result = _context.Orders.Add(order).Entity;
            _context.SaveChanges();
            return result;
        }

        public Order Delete(int orderId)
        {
            var result = ReadById(orderId);
            if (result != null)
            {
                return _context.Orders.Remove(result).Entity;
            }
            return null;
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
            throw new NotImplementedException();
        }
    }
}
