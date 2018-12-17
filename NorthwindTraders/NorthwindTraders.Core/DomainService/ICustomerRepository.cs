using NorthwindTraders.Core.Entity;
using System.Collections.Generic;

namespace NorthwindTraders.Core.DomainService
{
    public interface ICustomerRepository
    {
        Customer Create(Customer customer);
        Customer ReadById(string customerId);
        IEnumerable<Customer> ReadAll();
        Customer Update(Customer customerUpdate);
        Customer Delete(string customerId);
        Customer ReadByIdIncludeOrders(string customerId);
        IEnumerable<Customer> ReadAll(Filter filter = null);
        int Count();
    }
}
