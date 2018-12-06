using FluentAssertions;
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
    public class CustomerServiceTests
    {
        private CustomerService customerService;
        private Mock<ICustomerRepository> customerRepository = new Mock<ICustomerRepository>();
        private OrderService orderService;
        private Mock<IOrderRepository> orderRepository = new Mock<IOrderRepository>();
        private string address;
        private string city;
        private string companyName;
        private string contactName;
        private string contactTitle;
        private string country;
        private string customerID;
        private string fax;
        private string phone;
        private string postalCode;
        private string region;

        void Initialize()
        {
            //var serviceCollection = new ServiceCollection();
            //serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
            customerService = new CustomerService(customerRepository.Object,orderRepository.Object);
            orderService = new OrderService(orderRepository.Object);
            address = "123 Fake St";
            city = "Fake City";
            companyName = "Test Co";
            contactName = "Bob Tester";
            contactTitle = "CEO";
            country = "USA";
            customerID = "TESTC";
            fax = "555-555-4444";
            phone = "555-555-5555";
            postalCode = "43140";
            region = "Ohio";
        }

        #region NewCustomer Tests
        [TestMethod]
        [TestCategory("Unit")]
        public void NewCustomer_ValidCustomer_CustomerWithCorrectValues()
        {
            // Arrange
            Initialize();

            // Act
            var result = customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

            // Assert
            result.Address.Should().Be(address);
            result.City.Should().Be(city);
            result.CompanyName.Should().Be(companyName);
            result.ContactName.Should().Be(contactName);
            result.ContactTitle.Should().Be(contactTitle);
            result.Country.Should().Be(country);
            result.CustomerID.Should().Be(customerID);
            result.Fax.Should().Be(fax);
            result.Phone.Should().Be(phone);
            result.PostalCode.Should().Be(postalCode);
            result.Region.Should().Be(region);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewCustomer_CustomerIDNull_ThrowNullArgumentError()
        {
            // Arrange
            Initialize();
            customerID = null;

            // Act
            Action result = () => customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

            // Assert
            result.Should().Throw<ArgumentNullException>().WithMessage("Customer ID cannot be null, empty, or whitespace\nParameter name: Customer ID");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewCustomer_CustomerIDNotExactly5Characters_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            customerID = "1234567";

            // Act
            Action result = () => customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

            // Assert
            result.Should().Throw<ArgumentException>().WithMessage("Customer ID must be exactly 5 characters in length\nParameter name: Customer ID");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewCustomer_CompanyNameTooLong_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            companyName = "12345678901234567890123456789012345678901234567890";

            // Act
            Action result = () => customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

            // Assert
            result.Should().Throw<ArgumentException>().WithMessage("Company Name length must be no less than 1 and no more than 40 characters\nParameter name: Company Name");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewCustomer_CompanyNameNull_ThrowNullArgumentError()
        {
            // Arrange
            Initialize();
            companyName = null;

            // Act
            Action result = () => customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

            // Assert
            result.Should().Throw<ArgumentNullException>().WithMessage("Company Name cannot be null, empty, or whitespace\nParameter name: Company Name");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewCustomer_ContactNameTooLong_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            contactName = "12345678901234567890123456789012345678901234567890";

            // Act
            Action result = () => customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

            // Assert
            result.Should().Throw<ArgumentException>().WithMessage("Contact Name must be no more than 30 characters in length\nParameter name: Contact Name");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewCustomer_ContactTitleTooLong_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            contactTitle = "12345678901234567890123456789012345678901234567890";

            // Act
            Action result = () => customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

            // Assert
            result.Should().Throw<ArgumentException>().WithMessage("Contact Title must be no more than 30 characters in length\nParameter name: Contact Title");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewCustomer_AddressTooLong_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            address = "1234567890123456789012345678901234567890123456789012345678901234567890";

            // Act
            Action result = () => customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

            // Assert
            result.Should().Throw<ArgumentException>().WithMessage("Address must be no more than 60 characters in length\nParameter name: Address");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewCustomer_CityTooLong_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            city = "12345678901234567890";

            // Act
            Action result = () => customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

            // Assert
            result.Should().Throw<ArgumentException>().WithMessage("City must be no more than 15 characters in length\nParameter name: City");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewCustomer_RegionTooLong_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            region = "12345678901234567890";

            // Act
            Action result = () => customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

            // Assert
            result.Should().Throw<ArgumentException>().WithMessage("Region must be no more than 15 characters in length\nParameter name: Region");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewCustomer_PostalCodeTooLong_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            postalCode = "12345678901234567890";

            // Act
            Action result = () => customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

            // Assert
            result.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Postal Code must be no more than 10 characters in length\nParameter name: Postal Code");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewCustomer_CountryTooLong_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            country = "12345678901234567890";

            // Act
            Action result = () => customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

            // Assert
            result.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Country must be no more than 15 characters in length\nParameter name: Country");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewCustomer_PhoneTooLong_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            phone = "123456789012345678901234567890";

            // Act
            Action result = () => customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

            // Assert
            result.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Phone must be no more than 24 characters in length\nParameter name: Phone");
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void NewCustomer_FaxTooLong_ThrowInvalidArgumentError()
        {
            // Arrange
            Initialize();
            fax = "123456789012345678901234567890";

            // Act
            Action result = () => customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

            // Assert
            result.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Fax must be no more than 24 characters in length\nParameter name: Fax");
        }
        #endregion

        #region CreateCustomer Tests
        [TestMethod]
        [TestCategory("Unit")]
        public void CreateCustomer_ValidCustomer_NoErrors()
        {
            // Arrange
            Initialize();
            Customer customer = customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);
            customerRepository.Setup(m => m.Create(It.IsAny<Customer>())).Returns(customer);

            // Act
            var result = customerService.CreateCustomer(customer);

            // Assert
            result.Should().BeEquivalentTo(customer);
            customerRepository.Verify(m => m.Create(It.IsAny<Customer>()), Times.Once);
        }
        #endregion

        #region FindById Tests
        [TestMethod]
        [TestCategory("Unit")]
        public void FindById_ValidCustomerId_CorrectCustomer()
        {
            // Arrange
            Initialize();
            Customer customer = customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);
            customerRepository.Setup(m => m.ReadById(It.IsAny<string>())).Returns(customer);

            // Act
            var result = customerService.FindCustomerById(customer.CustomerID);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(customer);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void FindById_NonExistentCustomerId_NullCustomer()
        {
            // Arrange
            Initialize();
            Customer customer = null;
            customerRepository.Setup(m => m.ReadById(It.IsAny<string>())).Returns(customer);

            // Act
            var result = customerService.FindCustomerById("NOPE!");

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void FindById_NullCustomerId_NullArgumentException()
        {
            // Arrange
            Initialize();

            // Act
            Action result = () => customerService.FindCustomerById(null);

            // Assert
            result.Should().Throw<ArgumentNullException>().WithMessage("Customer ID cannot be null, empty, or whitespace\nParameter name: Customer ID");
        }

        [TestMethod]
        [TestCategory("Undefined")]
        public void FindByIdIncludeOrders_ValidCustomer_CustomerWithOrders()
        {
            // Arrange
            Initialize();
            Customer customer = customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);
            Shipper shipper = new Shipper() { Id = 1, CompanyName = "FredEx", Phone = "111-111-1111" };
            Order order = orderService.NewOrder(1,
                DateTime.Now.AddDays(-1),
                customer,
                DateTime.Now.AddDays(10),
                DateTime.Now,
                shipper,
                (decimal)15.65,
                customer.CompanyName,
                customer.Address,
                customer.City,
                customer.Region,
                customer.PostalCode,
                customer.Country,
                null);
            List<Order> orders = new List<Order>();
            orders.Add(order);
            customer.Orders = orders;
            customerRepository.Setup(m => m.ReadById(It.IsAny<string>())).Returns(customer);
            orderRepository.Setup(m => m.ReadAll()).Returns(orders);

            // Act
            var result = customerService.FindCustomerByIdIncludingOrders(customer.CustomerID);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(customer);
        }
        #endregion

        #region GetAllCustomersTests
        [TestMethod]
        [TestCategory("Unit")]
        public void GetAllCustomers_CustomersInRepository_ListOfCustomers()
        {
            // Arrange
            Initialize();
            IEnumerable<Customer> customers = new List<Customer>()
            {
                new Customer()
                {
                    Address = "123 Wubba St",
                    City = "Test One City",
                    CompanyName = "Test Co One",
                    ContactName = "Test Contact One",
                    ContactTitle = "Title One",
                    Country = "USA",
                    CustomerID = "ONE01",
                    Fax = "123-456-7890",
                    Phone = "123-456-7890",
                    PostalCode = "44444",
                    Region = "Region One"
                },
                new Customer()
                {
                    Address = "123 Two St",
                    City = "Test Two City",
                    CompanyName = "Test Co Two",
                    ContactName = "Test Contact Two",
                    ContactTitle = "Title Two",
                    Country = "USA",
                    CustomerID = "TWO02",
                    Fax = "123-456-7890",
                    Phone = "123-456-7890",
                    PostalCode = "33333",
                    Region = "Region Two"
                }
            };
            customerRepository.Setup(m => m.ReadAll()).Returns(customers);

            // Act
            var result = customerService.GetAllCustomers();

            // Assert
            result.Should().NotBeNullOrEmpty().And.HaveCount(2);
            result.Should().BeEquivalentTo(customers);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetAllCustomers_NoCustomersInRepository_EmptyListOfCustomers()
        {
            // Arrange
            Initialize();
            IEnumerable<Customer> customers = new List<Customer>();

            customerRepository.Setup(m => m.ReadAll()).Returns(customers);

            // Act
            var result = customerService.GetAllCustomers();

            // Assert
            result.Should().BeEmpty().And.HaveCount(0);
        }
        #endregion

        #region UpdateCustomer Tests
        [TestMethod]
        [TestCategory("Unit")]
        public void UpdateCustomer_ValidCustomer_CorrectCustomerReturned()
        {
            // Arrange
            Initialize();
            Customer updateCustomer = customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);
            customerRepository.Setup(m => m.ReadById(It.IsAny<string>())).Returns(updateCustomer);
            customerRepository.Setup(m => m.Update(It.IsAny<Customer>())).Returns(updateCustomer);

            // Act
            var result = customerService.UpdateCustomer(updateCustomer);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(updateCustomer);
            customerRepository.Verify(m => m.ReadById(It.IsAny<string>()), Times.Once);
            customerRepository.Verify(m => m.Update(It.IsAny<Customer>()), Times.Once);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void UpdateCustomer_NonexistentCustomer_CustomerNotFoundError()
        {
            // Arrange
            Initialize();
            Customer updateCustomer = customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);
            customerRepository.Setup(m => m.ReadById(It.IsAny<string>())).Returns((Customer)null);

            // Act
            Action result = () => customerService.UpdateCustomer(updateCustomer);

            // Assert
            result.Should().Throw<Exception>().WithMessage("Customer ID 'TESTC' not found to update");
            customerRepository.Verify(m => m.ReadById(It.IsAny<string>()), Times.Once);
            customerRepository.Verify(m => m.Update(It.IsAny<Customer>()), Times.Never);
        }
        #endregion

        #region DeleteCustomer Tests
        [TestMethod]
        [TestCategory("Undefined")]
        public void DeleteCustomer_ValidCustomerID_CorrectCustomerReturned()
        {
            // Arrange
            Initialize();
            Customer deleteCustomer = customerService.NewCustomer(customerID, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);
            customerRepository.Setup(m => m.Delete(It.IsAny<string>())).Returns(deleteCustomer);

            // Act
            var result = customerService.DeleteCustomer(deleteCustomer.CustomerID);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(deleteCustomer);
            customerRepository.Verify(m => m.Delete(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void DeleteCustomer_InvalidCustomerID_InvalidCustomerIdError()
        {
            // Arrange
            Initialize();

            // Act
            Action result = () => customerService.DeleteCustomer(null);

            // Assert
            result.Should().Throw<ArgumentException>().WithMessage("Customer ID for delete cannot be null, empty, or whitespace\nParameter name: Customer ID");
            customerRepository.Verify(m => m.Delete(It.IsAny<string>()), Times.Never);
        }
        #endregion
    }
}
