using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NorthwindTraders.Core.ApplicationService.Services;
using NorthwindTraders.Core.DomainService;
using NorthwindTraders.Core.Entity;
using System;

namespace NorthwindTraders.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        private CustomerService customerService;
        private Mock<ICustomerRepository> customerRepository = new Mock<ICustomerRepository>();
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
            customerService = new CustomerService(customerRepository.Object);
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

        [TestMethod]
        [TestCategory("Unit")]
        public void NewCustomer_ValidCustomer_CustomerWithCorrectValues()
        {
            // Arrange
            Initialize();
            //customerRepository.Setup

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
            customerID = "1234567Foo";

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
    }
}
