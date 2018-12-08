using Microsoft.AspNetCore.Mvc;
using NorthwindTraders.Core.ApplicationService;
using NorthwindTraders.Core.Entity;
using System;
using System.Collections.Generic;

namespace NorthwindTraders.RESTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            List<Order> fetchOrders;

            try
            {
                fetchOrders = _orderService.GetAllOrders();
                if (fetchOrders is null)
                {
                    return NoContent();
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return fetchOrders;
        }

        // GET: api/Orders/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Order> Get(int id)
        {
            Order fetchOrder;

            try
            {
                fetchOrder = _orderService.FindOrderById(id);
                if (fetchOrder is null)
                {
                    return NotFound(id);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return fetchOrder;
        }

        // POST: api/Orders
        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order order)
        {
            Order createdOrder;

            if (MissingOrInvalid(order.Customer.CustomerID))
            {
                return BadRequest("Order must have a valid Customer ID (exactly 5 characters)");
            }
            try
            {
                createdOrder = _orderService.CreateOrder(order);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created($"/api/orders/{createdOrder.OrderId}",createdOrder);
        }

        private bool MissingOrInvalid(string customerID)
        {
            if (string.IsNullOrEmpty(customerID))
            {
                return true;
            }

            if (customerID.Length != 5)
            {
                return true;
            }
            return false;
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id, [FromBody] Order order)
        {
            if (id < 1 || id != order.OrderId)
            {
                return BadRequest("Parameter Id and order ID must be the same");
            }

            return Ok(_orderService.UpdateOrder(order));
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public ActionResult<Order> Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest("Order ID parameter must be greater than zero");
            }
            try
            {
                return Ok(_orderService.DeleteOrder(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
