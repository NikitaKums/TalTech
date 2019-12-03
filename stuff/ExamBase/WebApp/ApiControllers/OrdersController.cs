using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using ee.itcollege.nikita.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        public OrdersController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }


        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders(string search)
        {
            return Ok(await _uow.Orders.AllAsyncWithSearchAPI(User.GetUserId(), search));
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DAL.App.DTO.Order>> GetOrder(int id)
        {
            var order = await _uow.Orders.FindAsyncByIdAPI(id, User.GetUserId());

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            order.AppUserId = User.GetUserId();
            _uow.Orders.Update(order);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            order.OrderState = OrderState.Waiting;
            order.AppUserId = User.GetUserId();
            await _uow.Orders.AddAsync(order);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _uow.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _uow.Orders.Remove(order);
            await _uow.SaveChangesAsync();

            return NoContent();
        }
    }
}
