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

        //[TestMethod]
        //[TestCategory("Unit")]
        //public void NewOrder_CompanyNameNull_ThrowNullArgumentError()
        //{
        //    // Arrange
        //    Initialize();
        //    companyName = null;

        //    // Act
        //    Action result = () => OrderService.NewOrder(OrderID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

        //    // Assert
        //    result.Should().Throw<ArgumentNullException>().WithMessage("Company Name cannot be null, empty, or whitespace\nParameter name: Company Name");
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //public void NewOrder_ContactNameTooLong_ThrowInvalidArgumentError()
        //{
        //    // Arrange
        //    Initialize();
        //    contactName = "12345678901234567890123456789012345678901234567890";

        //    // Act
        //    Action result = () => OrderService.NewOrder(OrderID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

        //    // Assert
        //    result.Should().Throw<ArgumentException>().WithMessage("Contact Name must be no more than 30 characters in length\nParameter name: Contact Name");
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //public void NewOrder_ContactTitleTooLong_ThrowInvalidArgumentError()
        //{
        //    // Arrange
        //    Initialize();
        //    contactTitle = "12345678901234567890123456789012345678901234567890";

        //    // Act
        //    Action result = () => OrderService.NewOrder(OrderID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

        //    // Assert
        //    result.Should().Throw<ArgumentException>().WithMessage("Contact Title must be no more than 30 characters in length\nParameter name: Contact Title");
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //public void NewOrder_AddressTooLong_ThrowInvalidArgumentError()
        //{
        //    // Arrange
        //    Initialize();
        //    address = "1234567890123456789012345678901234567890123456789012345678901234567890";

        //    // Act
        //    Action result = () => OrderService.NewOrder(OrderID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

        //    // Assert
        //    result.Should().Throw<ArgumentException>().WithMessage("Address must be no more than 60 characters in length\nParameter name: Address");
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //public void NewOrder_CityTooLong_ThrowInvalidArgumentError()
        //{
        //    // Arrange
        //    Initialize();
        //    city = "12345678901234567890";

        //    // Act
        //    Action result = () => OrderService.NewOrder(OrderID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

        //    // Assert
        //    result.Should().Throw<ArgumentException>().WithMessage("City must be no more than 15 characters in length\nParameter name: City");
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //public void NewOrder_RegionTooLong_ThrowInvalidArgumentError()
        //{
        //    // Arrange
        //    Initialize();
        //    region = "12345678901234567890";

        //    // Act
        //    Action result = () => OrderService.NewOrder(OrderID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

        //    // Assert
        //    result.Should().Throw<ArgumentException>().WithMessage("Region must be no more than 15 characters in length\nParameter name: Region");
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //public void NewOrder_PostalCodeTooLong_ThrowInvalidArgumentError()
        //{
        //    // Arrange
        //    Initialize();
        //    postalCode = "12345678901234567890";

        //    // Act
        //    Action result = () => OrderService.NewOrder(OrderID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

        //    // Assert
        //    result.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Postal Code must be no more than 10 characters in length\nParameter name: Postal Code");
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //public void NewOrder_CountryTooLong_ThrowInvalidArgumentError()
        //{
        //    // Arrange
        //    Initialize();
        //    country = "12345678901234567890";

        //    // Act
        //    Action result = () => OrderService.NewOrder(OrderID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

        //    // Assert
        //    result.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Country must be no more than 15 characters in length\nParameter name: Country");
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //public void NewOrder_PhoneTooLong_ThrowInvalidArgumentError()
        //{
        //    // Arrange
        //    Initialize();
        //    phone = "123456789012345678901234567890";

        //    // Act
        //    Action result = () => OrderService.NewOrder(OrderID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

        //    // Assert
        //    result.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Phone must be no more than 24 characters in length\nParameter name: Phone");
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //public void NewOrder_FaxTooLong_ThrowInvalidArgumentError()
        //{
        //    // Arrange
        //    Initialize();
        //    fax = "123456789012345678901234567890";

        //    // Act
        //    Action result = () => OrderService.NewOrder(OrderID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

        //    // Assert
        //    result.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Fax must be no more than 24 characters in length\nParameter name: Fax");
        //}
        #endregion

        //#region CreateOrder Tests
        //[TestMethod]
        //[TestCategory("Unit")]
        //public void CreateOrder_ValidOrder_NoErrors()
        //{
        //    // Arrange
        //    Initialize();
        //    Order Order = OrderService.NewOrder(OrderID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);
        //    OrderRepository.Setup(m => m.Create(It.IsAny<Order>())).Returns(Order);

        //    // Act
        //    var result = OrderService.CreateOrder(Order);

        //    // Assert
        //    result.Should().BeEquivalentTo(Order);
        //    OrderRepository.Verify(m => m.Create(It.IsAny<Order>()), Times.Once);
        //}
        //#endregion

        //#region FindById Tests
        //[TestMethod]
        //[TestCategory("Unit")]
        //public void FindById_ValidOrderId_CorrectOrder()
        //{
        //    // Arrange
        //    Initialize();
        //    Order Order = OrderService.NewOrder(OrderID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);
        //    OrderRepository.Setup(m => m.ReadById(It.IsAny<string>())).Returns(Order);

        //    // Act
        //    var result = OrderService.FindOrderById(Order.OrderID);

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.Should().BeEquivalentTo(Order);
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //public void FindById_NonExistentOrderId_NullOrder()
        //{
        //    // Arrange
        //    Initialize();
        //    Order Order = null;
        //    OrderRepository.Setup(m => m.ReadById(It.IsAny<string>())).Returns(Order);

        //    // Act
        //    var result = OrderService.FindOrderById("NOPE!");

        //    // Assert
        //    result.Should().BeNull();
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //public void FindById_NullOrderId_NullArgumentException()
        //{
        //    // Arrange
        //    Initialize();

        //    // Act
        //    Action result = () => OrderService.FindOrderById(null);

        //    // Assert
        //    result.Should().Throw<ArgumentNullException>().WithMessage("Order ID cannot be null, empty, or whitespace\nParameter name: Order ID");
        //}
        //#endregion

        //#region GetAllOrdersTests
        //[TestMethod]
        //[TestCategory("Unit")]
        //public void GetAllOrders_OrdersInRepository_ListOfOrders()
        //{
        //    // Arrange
        //    Initialize();
        //    IEnumerable<Order> Orders = new List<Order>()
        //    {
        //        new Order()
        //        {
        //            Address = "123 Wubba St",
        //            City = "Test One City",
        //            CompanyName = "Test Co One",
        //            ContactName = "Test Contact One",
        //            ContactTitle = "Title One",
        //            Country = "USA",
        //            OrderID = "ONE01",
        //            Fax = "123-456-7890",
        //            Phone = "123-456-7890",
        //            PostalCode = "44444",
        //            Region = "Region One"
        //        },
        //        new Order()
        //        {
        //            Address = "123 Two St",
        //            City = "Test Two City",
        //            CompanyName = "Test Co Two",
        //            ContactName = "Test Contact Two",
        //            ContactTitle = "Title Two",
        //            Country = "USA",
        //            OrderID = "TWO02",
        //            Fax = "123-456-7890",
        //            Phone = "123-456-7890",
        //            PostalCode = "33333",
        //            Region = "Region Two"
        //        }
        //    };
        //    OrderRepository.Setup(m => m.ReadAll()).Returns(Orders);

        //    // Act
        //    var result = OrderService.GetAllOrders();

        //    // Assert
        //    result.Should().NotBeNullOrEmpty().And.HaveCount(2);
        //    result.Should().BeEquivalentTo(Orders);
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //public void GetAllOrders_NoOrdersInRepository_EmptyListOfOrders()
        //{
        //    // Arrange
        //    Initialize();
        //    IEnumerable<Order> Orders = new List<Order>();

        //    OrderRepository.Setup(m => m.ReadAll()).Returns(Orders);

        //    // Act
        //    var result = OrderService.GetAllOrders();

        //    // Assert
        //    result.Should().BeEmpty().And.HaveCount(0);
        //}
        //#endregion

        //#region UpdateOrder Tests
        //[TestMethod]
        //[TestCategory("Unit")]
        //public void UpdateOrder_ValidOrder_CorrectOrderReturned()
        //{
        //    // Arrange
        //    Initialize();
        //    Order updateOrder = OrderService.NewOrder(OrderID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);
        //    OrderRepository.Setup(m => m.ReadById(It.IsAny<string>())).Returns(updateOrder);
        //    OrderRepository.Setup(m => m.Update(It.IsAny<Order>())).Returns(updateOrder);

        //    // Act
        //    var result = OrderService.UpdateOrder(updateOrder);

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.Should().BeEquivalentTo(updateOrder);
        //    OrderRepository.Verify(m => m.ReadById(It.IsAny<string>()), Times.Once);
        //    OrderRepository.Verify(m => m.Update(It.IsAny<Order>()), Times.Once);
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //public void UpdateOrder_NonexistentOrder_OrderNotFoundError()
        //{
        //    // Arrange
        //    Initialize();
        //    Order updateOrder = OrderService.NewOrder(OrderID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);
        //    OrderRepository.Setup(m => m.ReadById(It.IsAny<string>())).Returns((Order)null);

        //    // Act
        //    Action result = () => OrderService.UpdateOrder(updateOrder);

        //    // Assert
        //    result.Should().Throw<Exception>().WithMessage("Order ID 'TESTC' not found to update");
        //    OrderRepository.Verify(m => m.ReadById(It.IsAny<string>()), Times.Once);
        //    OrderRepository.Verify(m => m.Update(It.IsAny<Order>()), Times.Never);
        //}
        //#endregion

        //#region DeleteOrder Tests
        //[TestMethod]
        //[TestCategory("Undefined")]
        //public void DeleteOrder_ValidOrderID_CorrectOrderReturned()
        //{
        //    // Arrange
        //    Initialize();
        //    Order deleteOrder = OrderService.NewOrder(OrderID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);
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
        //#endregion
    }
}
