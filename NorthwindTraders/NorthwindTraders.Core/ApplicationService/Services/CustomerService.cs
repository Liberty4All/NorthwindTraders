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

        public Customer NewCustomer(string customerID, string companyName, string contactName, string contactTitle, string address, string city, string region, string postalCode, string country, string phone, string fax)
        {
            RaiseIfNullOrWhitespace("Customer ID", customerID);
            RaiseIfLengthWrong("Customer ID", customerID.Length, 5, 5);
            RaiseIfNullOrWhitespace("Company Name", companyName);
            RaiseIfLengthWrong("Company Name", companyName.Length, 1, 40);

            RaiseIfLengthWrong("Contact Name", contactName is null ? 0 : contactName.Length, 0, 30);
            RaiseIfLengthWrong("Contact Title", contactTitle is null ? 0 : contactTitle.Length, 0, 30);
            RaiseIfLengthWrong("Address", address is null ? 0 : address.Length, 0, 60); 
            RaiseIfLengthWrong("City", city is null ? 0 : city.Length, 0, 15);
            RaiseIfLengthWrong("Region", region is null ? 0 : region.Length, 0, 15);

            RaiseIfLengthWrong("Country", country is null ? 0 : country.Length, 0, 15);
            RaiseIfLengthWrong("Fax", fax is null ? 0 : fax.Length, 0, 24);
            RaiseIfLengthWrong("Phone", phone is null ? 0 : phone.Length, 0, 24);
            RaiseIfLengthWrong("Postal Code", postalCode is null ? 0 : postalCode.Length, 0, 10);

            var result = new Customer()
            {
                CustomerID = customerID,
                CompanyName = companyName,
                ContactName = contactName,
                ContactTitle = contactTitle,
                Address = address,
                City = city,
                Region = region,
                PostalCode = postalCode,
                Country = country,
                Phone = phone,
                Fax = fax
            };
            return result;
        }

        private void RaiseIfLengthWrong(string paramName, int paramLength, int minLength, int maxLength)
        {
            if (paramLength < minLength || paramLength > maxLength)
            {
                if (minLength == maxLength)
                {
                    throw new ArgumentOutOfRangeException(paramName, $"{paramName} must be exactly {minLength} characters in length");
                }
                if (minLength == 0 && paramLength > maxLength)
                {
                    throw new ArgumentOutOfRangeException(paramName, $"{paramName} must be no more than {maxLength} characters in length");
                }
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} length must be no less than {minLength} and no more than {maxLength} characters");
            }
        }

        private void RaiseIfNullOrWhitespace (string paramName, string paramValue)
        {
            if (string.IsNullOrWhiteSpace(paramValue))
            {
                throw new ArgumentNullException(paramName, $"{paramName} cannot be null, empty, or whitespace");
            }
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
