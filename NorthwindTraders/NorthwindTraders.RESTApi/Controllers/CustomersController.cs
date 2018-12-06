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
            return _customerService.GetAllCustomers();
        }

        // GET api/customers/KEENE
        [HttpGet("{customerID}")]
        public ActionResult<Customer> Get(string customerID)
        {
            return _customerService.FindCustomerByIdIncludingOrders(customerID);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Customer customer)
        {
            _customerService.CreateCustomer(customer);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{customerID}")]
        public ActionResult<Customer> Delete(string customerID)
        {
            return _customerService.DeleteCustomer(customerID);
        }
    }
}
