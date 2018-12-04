using NorthwindTraders.Core.DomainService;
using NorthwindTraders.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindTraders.Infrastructure.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public Order Create(Order customer)
        {
            throw new NotImplementedException();
        }

        public Order Delete(int orderId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Order ReadById(int orderId)
        {
            throw new NotImplementedException();
        }

        public Order Update(Order orderUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
