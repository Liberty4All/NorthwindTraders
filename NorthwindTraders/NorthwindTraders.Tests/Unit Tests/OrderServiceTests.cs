using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NorthwindTraders.Core.ApplicationService.Services;
using NorthwindTraders.Core.DomainService;
using NorthwindTraders.Core.Entity;
using System;
using System.Collections.Generic;

namespace NorthwindTraders.Tests
{
    [TestClass]
    public class OrderServiceTests
    {
        private OrderService orderService;
        private Mock<IOrderRepository> orderRepository = new Mock<IOrderRepository>();
        public int Id;
        public DateTime orderDate;
        public Customer customer;
        public DateTime requiredDate;
        public DateTime shippedDate;
        public Shipper shipper;
        public double freight;
        public string shipName;
        public string shipAddress;
        public string shipCity;
        public string shipRegion;
        public string shipPostalCode;
        public string shipCountry;
        public Employee employee;

        void Initialize()
        {
            Id = 0;
            orderService = new OrderService(orderRepository.Object);
            orderDate = DateTime.Now.AddDays(-100);
            customer = new Customer()
            {
                
                Address = "123 Test St",
                City = "Fake City",
                CompanyName = "Test Co",
                ContactName = "Bob Tester",
                ContactTitle = "Owner",
                Country = "USA",
                CustomerID = "TEST1",
                Fax = "555-444-3333",
                Orders = new List<Order>(),
                Phone = "555-666-7777",
                PostalCode = "55555",
                Region = "Region1"
            };
            requiredDate = orderDate.AddDays(14);
            shippedDate = orderDate.AddDays(3);
            shipper = new Shipper();
            freight = 12.5;
            shipName = "Bubba Express";
            shipAddress = "123 Ship Street";
            shipCity = "Ship City";
            shipRegion = "Ship Region";
            shipPostalCode = "44444";
            shipCountry = "USA";
            employee = new Employee();
        }

        #region NewOrder Tests
        [TestMethod]
        [TestCategory("Unit")]
        public void NewOrder_ValidOrder_OrderWithCorrectValues()
        {
            // Arrange
            Initialize();

            // Act
            var result = orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);

            // Assert
            result.Customer.Should().BeEquivalentTo(customer);
            result.Employee.Should().BeEquivalentTo(employee);
            result.Freight.Should().Be(freight);
            result.Id.Should().Be(Id);
            result.OrderDate.Should().Be(orderDate);
            result.RequiredDate.Should().Be(requiredDate);
            result.ShipAddress.Should().Be(shipAddress);
            result.ShipCity.Should().Be(shipCity);
            result.ShipCountry.Should().Be(shipCountry);
            result.ShipName.Should().Be(shipName);
            result.ShippedDate.Should().Be(shippedDate);
            result.Shipper.Should().BeEquivalentTo(shipper);
            result.ShipPostalCode.Should().Be(shipPostalCode);
            result.ShipRegion.Should().Be(shipRegion);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewOrder_OrderIDNegative_ThrowNullArgumentError()
        {
            // Arrange
            Initialize();
            Id = -1;

            // Act
            Action result = () => orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);

            // Assert
            result.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Order ID cannot be less than 0\nParameter name: Order ID");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewOrder_OrderDateTooOld_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            orderDate = 31.December(1995);

            // Act
            Action result = () => orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);

            // Assert
            result.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Order Date cannot be before January 1, 1996\nParameter name: Order Date");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewOrder_OrderDateInTheFuture_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            orderDate = DateTime.Now.Date.AddDays(5);

            // Act
            Action result = () => orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);

            // Assert
            result.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Order Date cannot be in the future\nParameter name: Order Date");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewOrder_CustomerNull_ThrowNullArgumentError()
        {
            // Arrange
            Initialize();
            customer = null;

            // Act
            Action result = () => orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);

            // Assert
            result.Should().Throw<ArgumentNullException>().WithMessage("Customer cannot be null\nParameter name: Order Customer");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewOrder_RequireDateBeforeOrderDate_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            requiredDate = orderDate.AddDays(-1);
            string orderDateString = orderDate.ToString("MMMM d, yyyy");

            // Act
            Action result = () => orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);

            // Assert
            result.Should().Throw<ArgumentOutOfRangeException>().WithMessage($"Required date cannot be before order date of {orderDateString}\nParameter name: Required Date");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewOrder_ShippedDateBeforeOrderDate_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            shippedDate = orderDate.AddDays(-1);
            string orderDateString = orderDate.ToString("MMMM d, yyyy");

            // Act
            Action result = () => orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);

            // Assert
            result.Should().Throw<ArgumentOutOfRangeException>().WithMessage($"Shipped date cannot be before order date of {orderDateString}\nParameter name: Shipped Date");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewOrder_ShipNameTooLong_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            shipName = "12345678901234567890123456789012345678901234567890";

            // Act
            Action result = () => orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);

            // Assert
            result.Should().Throw<ArgumentException>().WithMessage("Ship name must be no more than 40 characters in length\nParameter name: Ship Name");
        }

        [TestMethod]
        [TestCategory("Undefined")]
        public void NewOrder_ShipAddressTooLong_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            shipAddress = "1234567890123456789012345678901234567890123456789012345678901234567890";

            // Act
            Action result = () => orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);

            // Assert
            result.Should().Throw<ArgumentException>().WithMessage("Ship address must be no more than 60 characters in length\nParameter name: Ship Address");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewOrder_ShipCityTooLong_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            shipCity = "12345678901234567890";

            // Act
            Action result = () => orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);

            // Assert
            result.Should().Throw<ArgumentException>().WithMessage("Ship city must be no more than 15 characters in length\nParameter name: Ship City");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewOrder_RegionTooLong_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            shipRegion = "12345678901234567890";

            // Act
            Action result = () => orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);

            // Assert
            result.Should().Throw<ArgumentException>().WithMessage("Ship region must be no more than 15 characters in length\nParameter name: Ship Region");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewOrder_PostalCodeTooLong_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            shipPostalCode = "12345678901234567890";

            // Act
            Action result = () => orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);

            // Assert
            result.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Ship postal code must be no more than 10 characters in length\nParameter name: Ship Postal Code");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewOrder_CountryTooLong_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            shipCountry = "12345678901234567890";

            // Act
            Action result = () => orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);

            // Assert
            result.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Ship country must be no more than 15 characters in length\nParameter name: Ship Country");
        }
        #endregion

        #region CreateOrder Tests
        [TestMethod]
        [TestCategory("Unit")]
        public void CreateOrder_ValidOrder_NoErrors()
        {
            // Arrange
            Initialize();
            Order Order = orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);
            orderRepository.Setup(m => m.Create(It.IsAny<Order>())).Returns(Order);

            // Act
            var result = orderService.CreateOrder(Order);

            // Assert
            result.Should().BeEquivalentTo(Order);
            orderRepository.Verify(m => m.Create(It.IsAny<Order>()), Times.Once);
        }
        #endregion

        #region FindById Tests
        [TestMethod]
        [TestCategory("Unit")]
        public void FindById_ValidOrderId_CorrectOrder()
        {
            // Arrange
            Initialize();
            Order order = orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);
            orderRepository.Setup(m => m.ReadById(It.IsAny<int>())).Returns(order);

            // Act
            var result = orderService.FindOrderById(order.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(order);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void FindById_NonExistentOrderId_NullOrder()
        {
            // Arrange
            Initialize();
            Order order = null;
            orderRepository.Setup(m => m.ReadById(It.IsAny<int>())).Returns(order);

            // Act
            var result = orderService.FindOrderById(9999);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void FindById_NegativeOrderId_NullArgumentException()
        {
            // Arrange
            Initialize();

            // Act
            Action result = () => orderService.FindOrderById(-1);

            // Assert
            result.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Order ID cannot be less than 0\nParameter name: Order ID");
        }
        #endregion

        #region GetAllOrdersTests
        [TestMethod]
        [TestCategory("Unit")]
        public void GetAllOrders_OrdersInRepository_ListOfOrders()
        {
            // Arrange
            Initialize();
            IEnumerable<Order> Orders = new List<Order>()
            {
                new Order()
                {
                    Customer = customer,
                    Employee = employee,
                    Freight = freight,
                    Id = 1,
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
                },
                new Order()
                {
                    Customer = customer,
                    Employee = employee,
                    Freight = freight,
                    Id = 2,
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

                }
            };
            orderRepository.Setup(m => m.ReadAll()).Returns(Orders);

            // Act
            var result = orderService.GetAllOrders();

            // Assert
            result.Should().NotBeNullOrEmpty().And.HaveCount(2);
            result.Should().BeEquivalentTo(Orders);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetAllOrders_NoOrdersInRepository_EmptyListOfOrders()
        {
            // Arrange
            Initialize();
            IEnumerable<Order> orders = new List<Order>();

            orderRepository.Setup(m => m.ReadAll()).Returns(orders);

            // Act
            var result = orderService.GetAllOrders();

            // Assert
            result.Should().BeEmpty().And.HaveCount(0);
        }
        #endregion

        #region UpdateOrder Tests
        [TestMethod]
        [TestCategory("Unit")]
        public void UpdateOrder_ValidOrder_CorrectOrderReturned()
        {
            // Arrange
            Initialize();
            Order updateOrder = orderService.NewOrder(9999, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);
            orderRepository.Setup(m => m.ReadById(It.IsAny<int>())).Returns(updateOrder);
            orderRepository.Setup(m => m.Update(It.IsAny<Order>())).Returns(updateOrder);

            // Act
            var result = orderService.UpdateOrder(updateOrder);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(updateOrder);
            orderRepository.Verify(m => m.ReadById(It.IsAny<int>()), Times.Once);
            orderRepository.Verify(m => m.Update(It.IsAny<Order>()), Times.Once);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void UpdateOrder_NonexistentOrder_OrderNotFoundError()
        {
            // Arrange
            Initialize();
            Id = 99;
            Order updateOrder = orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);
            orderRepository.Setup(m => m.ReadById(It.IsAny<int>())).Returns((Order)null);

            // Act
            Action result = () => orderService.UpdateOrder(updateOrder);

            // Assert
            result.Should().Throw<Exception>().WithMessage($"Order ID '{Id}' not found to update");
            orderRepository.Verify(m => m.ReadById(It.IsAny<int>()), Times.Once);
            orderRepository.Verify(m => m.Update(It.IsAny<Order>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void UpdateOrder_OrderIDLessThanOne_ArgumentOutOfRangeError()
        {
            // Arrange
            Initialize();
            Order updateOrder = orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);
            orderRepository.Setup(m => m.ReadById(It.IsAny<int>())).Returns((Order)null);

            // Act
            Action result = () => orderService.UpdateOrder(updateOrder);

            // Assert
            result.Should().Throw<Exception>().WithMessage("Order ID cannot be less than 1\nParameter name: Order ID");
            orderRepository.Verify(m => m.ReadById(It.IsAny<int>()), Times.Never);
            orderRepository.Verify(m => m.Update(It.IsAny<Order>()), Times.Never);
        }
        #endregion

        #region DeleteOrder Tests
        //[TestMethod]
        //[TestCategory("Undefined")]
        //public void DeleteOrder_ValidOrderID_CorrectOrderReturned()
        //{
        //    // Arrange
        //    Initialize();
        //    Order deleteOrder = orderService.NewOrder(Id, orderDate, customer, requiredDate, shippedDate, shipper, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, employee);
        //    OrderRepository.Setup(m => m.Delete(It.IsAny<string>())).Returns(deleteOrder);

        //    // Act
        //    var result = OrderService.DeleteOrder(deleteOrder.OrderID);

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.Should().BeEquivalentTo(deleteOrder);
        //    OrderRepository.Verify(m => m.Delete(It.IsAny<string>()), Times.Once);
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //public void DeleteOrder_InvalidOrderID_InvalidOrderIdError()
        //{
        //    // Arrange
        //    Initialize();

        //    // Act
        //    Action result = () => OrderService.DeleteOrder(null);

        //    // Assert
        //    result.Should().Throw<ArgumentException>().WithMessage("Order ID for delete cannot be null, empty, or whitespace\nParameter name: Order ID");
        //    OrderRepository.Verify(m => m.Delete(It.IsAny<string>()), Times.Never);
        //}
        #endregion
    }
}
