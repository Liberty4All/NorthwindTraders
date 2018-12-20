using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NorthwindTraders.Core.DomainService;
using NorthwindTraders.Core.Entity;

namespace NorthwindTraders.Core.ApplicationService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order NewOrder(int id,
                              DateTime orderDate,
                              Customer customer,
                              DateTime requiredDate,
                              DateTime shippedDate,
                              int shipVia,
                              decimal freight,
                              string shipName,
                              string shipAddress,
                              string shipCity,
                              string shipRegion,
                              string shipPostalCode,
                              string shipCountry,
                              Employee employee)
        {
            RaiseIfNegative("Order ID", id);
            RaiseIfDateTooOld("Order Date", orderDate, string.Empty, new DateTime(1996, 1, 1));
            RaiseIfDateInTheFuture("Order Date", orderDate);
            RaiseIfNull(customer);
            RaiseIfDateTooOld("Required Date", requiredDate, "order date", orderDate);
            RaiseIfDateTooOld("Shipped Date", shippedDate, "order date", orderDate);
            RaiseIfLengthWrong("Ship Name", shipName.Length, 0, 40);
            RaiseIfLengthWrong("Ship Address", shipAddress.Length, 0, 60);
            RaiseIfLengthWrong("Ship City", shipCity.Length, 0, 15);
            RaiseIfLengthWrong("Ship Region", shipRegion.Length, 0, 15);
            RaiseIfLengthWrong("Ship Postal Code", shipPostalCode.Length, 0, 10);
            RaiseIfLengthWrong("Ship Country", shipCountry.Length, 0, 15);
            RaiseIfMissingOrInvalid(customer.CustomerID);

            var result = new Order()
            {
                Customer = customer,
                Employee = employee,
                Freight = freight,
                OrderId = id,
                OrderDate = orderDate,
                RequiredDate = requiredDate,
                ShipAddress = shipAddress,
                ShipCity = shipCity,
                ShipCountry = shipCountry,
                ShipName = shipName,
                ShippedDate = shippedDate,
                ShipVia = shipVia,
                ShipPostalCode = shipPostalCode,
                ShipRegion = shipRegion
            };
            return result;
        }

        public Order CreateOrder(Order order)
        {
            RaiseIfNegative("Order ID", order.OrderId);
            Order newOrder = NewOrder(
                order.OrderId,
                order.OrderDate,
                order.Customer,
                order.RequiredDate,
                order.ShippedDate.GetValueOrDefault(DateTime.MinValue),
                order.ShipVia,
                order.Freight,
                order.ShipName,
                order.ShipAddress,
                order.ShipCity,
                order.ShipRegion,
                order.ShipPostalCode,
                order.ShipCountry,
                order.Employee);
            return _orderRepository.Create(order);
        }

        public Order FindOrderById(int orderId)
        {
            RaiseIfLessThanOne("Order ID", orderId);
            Order fetchOrder = _orderRepository.ReadById(orderId);
            if (fetchOrder == null)
            {
                throw new NotFoundException($"Could not find Order ID: '{orderId}'");
            }
            return fetchOrder;
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.ReadAll().ToList();
        }

        public Order UpdateOrder(Order orderUpdate)
        {
            RaiseIfLessThanOne("Order ID", orderUpdate.OrderId);
            Order order = FindOrderById(orderUpdate.OrderId);

            order.OrderId = orderUpdate.OrderId;
            order.OrderDate = orderUpdate.OrderDate;
            order.Customer = orderUpdate.Customer;
            order.RequiredDate = orderUpdate.RequiredDate;
            order.ShippedDate = orderUpdate.ShippedDate.GetValueOrDefault(DateTime.MinValue);
            order.ShipVia = orderUpdate.ShipVia;
            order.Freight = orderUpdate.Freight;
            order.ShipName = orderUpdate.ShipName;
            order.ShipAddress = orderUpdate.ShipAddress;
            order.ShipCity = orderUpdate.ShipCity;
            order.ShipRegion = orderUpdate.ShipRegion;
            order.ShipPostalCode = orderUpdate.ShipPostalCode;
            order.ShipCountry = orderUpdate.ShipCountry;
            order.Employee = orderUpdate.Employee;
            return _orderRepository.Update(orderUpdate);
        }

        public Order DeleteOrder(int id)
        {
            RaiseIfLessThanOne("Order ID", id);
            if (_orderRepository.ReadById(id) is null)
            {
                throw new NotFoundException($"Order ID: {id} not found to delete");
            }
            return _orderRepository.Delete(id);
        }

        private void RaiseIfMissingOrInvalid(string customerID)
        {
            if (string.IsNullOrEmpty(customerID) ||
                customerID.Length != 5)
            {
                throw new ArgumentException("Order must have a valid Customer ID (exactly 5 characters)");
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

        private void RaiseIfNull(Customer customer)
        {
            if (customer is null)
            {
                throw new ArgumentNullException("Order Customer", "Customer cannot be null");
            }
        }

        private void RaiseIfDateInTheFuture(string paramName, DateTime paramDate)
        {
            if (paramDate.Date > DateTime.Now.Date)
            {
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} cannot be in the future");
            }
        }

        private void RaiseIfDateTooOld(string paramName, DateTime paramDate, string thresholdParam, DateTime threshholdDate)
        {
            if (paramDate.Date < threshholdDate.Date)
            {
                if (string.IsNullOrWhiteSpace(thresholdParam))
                {
                    throw new ArgumentOutOfRangeException(paramName, $"{paramName} cannot be before {threshholdDate.ToString("MMMM d, yyyy")}");
                }
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} cannot be before {thresholdParam} of {threshholdDate.ToString("MMMM d, yyyy")}");
            }
        }

        private void RaiseIfLessThanOne(string paramName, int paramValue)
        {
            if (paramValue < 1)
            {
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} cannot be less than 1");
            }
        }

        private void RaiseIfNegative(string paramName, int paramValue)
        {
            if (paramValue < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} cannot be negative");
            }
        }
    }
}
