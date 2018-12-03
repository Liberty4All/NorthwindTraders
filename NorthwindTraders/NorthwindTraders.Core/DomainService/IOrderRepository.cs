using NorthwindTraders.Core.Entity;
using System.Collections.Generic;

namespace NorthwindTraders.Core.DomainService
{
    public interface IOrderRepository
    {
        Order Create(Order customer);
        Order ReadById(int orderId);
        IEnumerable<Order> ReadAll();
        Order Update(Order orderUpdate);
        Order Delete(int orderId);
    }
}
