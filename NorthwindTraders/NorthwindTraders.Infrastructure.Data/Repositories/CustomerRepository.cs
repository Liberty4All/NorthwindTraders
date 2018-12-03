using NorthwindTraders.Core.DomainService;
using NorthwindTraders.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindTraders.Infrastructure.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public Customer Create(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Customer Delete(string customerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Customer ReadById(string customerId)
        {
            throw new NotImplementedException();
        }

        public Customer Update(Customer customerUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
