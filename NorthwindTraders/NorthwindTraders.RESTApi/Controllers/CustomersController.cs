using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthwindTraders.Core.ApplicationService;
using NorthwindTraders.Core.Entity;

namespace NorthwindTraders.RESTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET api/customers
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            List<Customer> fetchCustomers;
            try
            {
                fetchCustomers = _customerService.GetAllCustomers();
                if (fetchCustomers is null)
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return fetchCustomers;
        }

        // GET api/customers/KEENE
        [HttpGet("{customerID}")]
        public ActionResult<Customer> Get(string customerID)
        {
            Customer fetchCustomer;
            try
            {
                fetchCustomer = _customerService.FindCustomerByIdIncludingOrders(customerID);
                if (fetchCustomer is null)
                {
                    return NotFound(customerID);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return fetchCustomer;
        }

        // POST api/customers
        [HttpPost]
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            Customer createdCustomer;
            if (string.IsNullOrWhiteSpace(customer.ContactName))
            {
                return BadRequest("Company Name is required for creating a Customer");
            }

            try
            {
                createdCustomer = _customerService.CreateCustomer(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created($"/api/customers/{createdCustomer.CustomerID}", createdCustomer);
        }

        // PUT api/customers/KEENE
        [HttpPut("{customerID}")]
        public ActionResult<Customer> Put(string customerID, [FromBody] Customer customerUpdate)
        {
            if (CustomerIDIsInvalid(customerID) ||
                CustomerIDIsInvalid(customerUpdate.CustomerID) ||
                string.Equals(customerUpdate.CustomerID.ToUpper(), customerUpdate.CustomerID))
            {
                return BadRequest("Parameter CustomerID must be exactly 5 characters and uppercase");
            }

            if (string.Equals(customerID.ToUpper(), customerUpdate.CustomerID) == false)
            {
                return BadRequest("Parameter customerID and Customer ID must match exactly");
            }

            try
            {
                return Ok(_customerService.UpdateCustomer(customerUpdate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool CustomerIDIsInvalid(string customerID)
        {
            return (string.IsNullOrWhiteSpace(customerID) || 
                customerID.Length != 5);
        }

        // DELETE api/customers/5
        [HttpDelete("{customerID}")]
        public ActionResult<Customer> Delete(string customerID)
        {
            if (CustomerIDIsInvalid(customerID))
            {
                return BadRequest("CustomerID parameter must be exactly 5 characters");
            }
            try
            {
                return Ok(_customerService.DeleteCustomer(customerID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
