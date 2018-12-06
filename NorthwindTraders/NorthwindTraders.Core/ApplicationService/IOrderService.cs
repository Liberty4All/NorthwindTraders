using NorthwindTraders.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindTraders.Core.ApplicationService
{
    public interface IOrderService
    {
        Order NewOrder(int Id,
            DateTime OrderDate,
            Customer Customer,
            DateTime RequiredDate,
            DateTime ShippedDate,
            Shipper Shipper,
            decimal Freight,
            string ShipName,
            string ShipAddress,
            string ShipCity,
            string ShipRegion,
            string ShipPostalCode,
            string ShipCountry,
            Employee Employee);

        Order CreateOrder(Order order);
        Order FindOrderById(int orderId);
        List<Order> GetAllOrders();
        Order UpdateOrder(Order orderUpdate);
        Order DeleteOrder(int id);
    }
}
