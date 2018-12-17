using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthwindTraders.Core;
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
        public ActionResult<IEnumerable<Customer>> Get([FromQuery] Filter filter)
        {
            List<Customer> fetchCustomers;
            try
            {
                fetchCustomers = _customerService.GetFilteredCustomers(filter);
                if (fetchCustomers is null)
                {
                    return NoContent();
                }
            return Ok(fetchCustomers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/customers/KEENE
        [HttpGet("{customerID}")]
        public ActionResult<Customer> Get(string customerID)
        {
            Customer fetchCustomer;
            try
            {
                fetchCustomer = _customerService.FindCustomerByIdIncludingOrders(customerID);
            }
            catch (NotFoundException ex)
            {
                return  NotFound(ex.Message);
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

            try
            {
                Customer createdCustomer = _customerService.CreateCustomer(customer);
                return Created($"/api/customers/{createdCustomer.CustomerID}", createdCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/customers/KEENE
        [HttpPut("{customerID}")]
        public ActionResult<Customer> Put(string customerID, [FromBody] Customer customerUpdate)
        {
            try
            {
                return Ok(_customerService.UpdateCustomer(customerUpdate));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/customers/5
        [HttpDelete("{customerID}")]
        public ActionResult<Customer> Delete(string customerID)
        {
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
