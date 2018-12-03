using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NorthwindTraders.Core.DomainService;
using NorthwindTraders.Core.Entity;

namespace NorthwindTraders.Core.ApplicationService.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer NewCustomer(string CustomerID, string CompanyName, string ContactName, string ContactTitle, string Address, string City, string Region, string PostalCode, string Country, string Phone, string Fax)
        {
            throw new NotImplementedException();
        }

        public Customer CreateCustomer(Customer cust)
        {
            throw new NotImplementedException();
        }

        public Customer FindCustomerById(string customerId)
        {
            return _customerRepository.ReadById(customerId);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.ReadAll().ToList();
        }

        public Customer UpdateCustomer(Customer customerUpdate)
        {
            var customer = FindCustomerById(customerUpdate.CustomerID);
            customer.Address = customerUpdate.Address;
            customer.City = customerUpdate.City;
            customer.CompanyName = customerUpdate.CompanyName;
            customer.ContactName = customerUpdate.ContactName;
            customer.ContactTitle = customerUpdate.ContactTitle;
            customer.Country = customerUpdate.Country;
            customer.CustomerID = customerUpdate.CustomerID;
            customer.Fax = customerUpdate.Fax;
            customer.Phone = customerUpdate.Phone;
            customer.PostalCode = customerUpdate.PostalCode;
            customer.Region = customerUpdate.Region;
            return _customerRepository.Update(customer);
        }

        public Customer DeleteCustomer(string customerId)
        {
            return _customerRepository.Delete(customerId);
        }
    }
}
