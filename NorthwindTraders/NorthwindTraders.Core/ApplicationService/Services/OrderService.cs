using System;
using System.Collections.Generic;
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

        public Order CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Order DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Order FindOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Order NewOrder(int id,
                              DateTime orderDate,
                              Customer customer,
                              DateTime requiredDate,
                              DateTime shippedDate,
                              Shipper shipper,
                              double freight,
                              string shipName,
                              string shipAddress,
                              string shipCity,
                              string shipRegion,
                              string shipPostalCode,
                              string shipCountry,
                              Employee employee)
        {
            RaiseIfNegative("Order ID", id);
            RaiseIfDateTooOld("Order Date", orderDate, new DateTime(1996, 1, 1));
            RaiseIfDateInTheFuture("Order Date", orderDate);

            var result = new Order()
            {
                Customer = customer,
                Employee = employee,
                Freight = freight,
                Id = id,
                OrderDate = orderDate,
                RequiredDate = requiredDate,
                ShipAddress = shipAddress,
                ShipCity = shipCity,
                ShipCountry = shipCountry,
                ShipName = shipName,
                ShippedDate = shippedDate,
                Shipper = shipper,
                ShipPostalCode = shipPostalCode,
                ShipRegion = shipRegion
            };
            return result;
        }

        private void RaiseIfDateInTheFuture(string paramName, DateTime paramDate)
        {
            if (paramDate.Date > DateTime.Now.Date)
            {
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} cannot be in the future");
            }
        }

        private void RaiseIfDateTooOld(string paramName, DateTime paramDate, DateTime threshholdDate)
        {
            if (paramDate.Date < threshholdDate.Date)
            {
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} cannot be before {threshholdDate.ToString("MMMM d, yyyy")}");
            }
        }

        private void RaiseIfNegative(string paramName, int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} cannot be less than 0");
            }
        }

        public Order UpdateOrder(Order orderUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
