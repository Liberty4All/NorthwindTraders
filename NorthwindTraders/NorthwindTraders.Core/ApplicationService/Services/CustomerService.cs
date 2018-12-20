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
        private readonly IOrderRepository _orderRepository;

        public CustomerService(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
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

        private void RaiseIfCustomerIDExists(string customerID)
        {
            if (_customerRepository.ReadById(customerID) != null)
            {
                throw new ArgumentException($"Customer ID: {customerID} already exists in database", "Customer ID");
            }
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

        public Customer CreateCustomer(Customer customer)
        {
            RaiseIfCustomerIDExists(customer.CustomerID);
            Customer newCustomer = NewCustomer(
                customer.CustomerID,
                customer.CompanyName,
                customer.ContactName,
                customer.ContactTitle,
                customer.Address,
                customer.City,
                customer.Region,
                customer.PostalCode,
                customer.Country,
                customer.Phone,
                customer.Fax);
            return _customerRepository.Create(customer);
        }

        public Customer FindCustomerById(string customerId)
        {
            RaiseIfNullOrWhitespace("Customer ID", customerId);
            Customer fetchCustomer = _customerRepository.ReadById(customerId);
            if (fetchCustomer is null)
            {
                throw new NotFoundException($"Could not find Customer ID: '{customerId}'");
            }
            return fetchCustomer;
        }

        public Customer FindCustomerByIdIncludingOrders(string customerId)
        {
            RaiseIfNullOrWhitespace("Customer ID", customerId);
            Customer fetchCustomer = _customerRepository.ReadByIdIncludeOrders(customerId);
            if (fetchCustomer is null)
            {
                throw new NotFoundException($"Could not find Customer ID: {customerId}");
            }
            return fetchCustomer;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.ReadAll().ToList();
        }

        public List<Customer> GetFilteredCustomers(Filter filter)
        {
            RaiseIfNegative("Current Page", filter.CurrentPage);
            RaiseIfNegative("ItemsPerPage", filter.ItemsPerPage);
            if (((filter.CurrentPage - 1) * filter.ItemsPerPage) >= _customerRepository.Count())
            {
                throw new System.IO.InvalidDataException("Index out of bounds, CurrentPage is too high");
            }
            return _customerRepository.ReadAll(filter).ToList();
        }

        private void RaiseIfNegative(string paramName, int paramValue)
        {
            if (paramValue < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} cannot be negative"); 
            }
        }

        public Customer UpdateCustomer(Customer customerUpdate)
        {
            Customer customer = FindCustomerById(customerUpdate.CustomerID);

            customer.CustomerID = customerUpdate.CustomerID;
            customer.CompanyName = customerUpdate.CompanyName;
            customer.ContactName = customerUpdate.ContactName;
            customer.ContactTitle = customerUpdate.ContactTitle;
            customer.Address = customerUpdate.Address;
            customer.City = customerUpdate.City;
            customer.Region = customerUpdate.Region;
            customer.PostalCode = customerUpdate.PostalCode;
            customer.Country = customerUpdate.Country;
            customer.Phone = customerUpdate.Phone;
            customer.Fax = customerUpdate.Fax;
            return _customerRepository.Update(customer);
        }

        public Customer DeleteCustomer(string customerId)
        {
            RaiseIfNullOrWhitespace("Customer ID", customerId);
            RaiseIfLengthWrong("Customer ID", customerId.Length, 5, 5);
            if (_customerRepository.ReadById(customerId) == null)
            {
                throw new NotFoundException($"Customer ID: {customerId} not found to delete");
            }
            return _customerRepository.Delete(customerId);
        }

    }
}
