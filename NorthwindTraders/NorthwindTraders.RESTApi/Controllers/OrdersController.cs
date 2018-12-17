using Microsoft.AspNetCore.Mvc;
using NorthwindTraders.Core;
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
            return Ok(fetchOrders);
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

            try
            {
                createdOrder = _orderService.CreateOrder(order);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created($"/api/orders/{createdOrder.OrderId}",createdOrder);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id, [FromBody] Order order)
        {
            try
            {
                if (id != order.OrderId)
                {
                    throw new ArgumentException("Parameter Id and order ID must be the same");
                }

                return Ok(_orderService.UpdateOrder(order));
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

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public ActionResult<Order> Delete(int id)
        {
            try
            {
                return Ok(_orderService.DeleteOrder(id));
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
    }
}
