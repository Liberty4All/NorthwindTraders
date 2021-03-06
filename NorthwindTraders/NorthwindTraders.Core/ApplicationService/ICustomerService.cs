﻿using NorthwindTraders.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindTraders.Core.ApplicationService
{
    public interface ICustomerService
    {
        Customer NewCustomer(string CustomerID,
                             string CompanyName,
                             string ContactName,
                             string ContactTitle,
                             string Address,
                             string City,
                             string Region,
                             string PostalCode,
                             string Country,
                             string Phone,
                             string Fax);
        Customer CreateCustomer(Customer cust);
        Customer FindCustomerById(string customerId);
        Customer FindCustomerByIdIncludingOrders(string customerId);
        List<Customer> GetFilteredCustomers(Filter filter);
        List<Customer> GetAllCustomers();
        Customer UpdateCustomer(Customer customerUpdate);
        Customer DeleteCustomer(string id);

    }
}