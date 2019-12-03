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
    public class PizzasInOrderController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        public PizzasInOrderController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }


        // GET: api/PizzasInOrder
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaInOrder>>> GetPizzasInOrder(string search)
        {
            return Ok(await _uow.PizzasInOrder.AllAsyncWithSearchAPI(User.GetUserId(), search));
        }

        // GET: api/PizzasInOrder/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DAL.App.DTO.PizzaInOrder>> GetPizzaInOrder(int id)
        {
            var pizzaInOrder = await _uow.PizzasInOrder.FindAsyncByIdAPI(id, User.GetUserId());

            if (pizzaInOrder == null)
            {
                return NotFound();
            }

            return pizzaInOrder;
        }

        // PUT: api/PizzasInOrder/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaInOrder(int id, PizzaInOrder pizzaInOrder)
        {
            if (id != pizzaInOrder.Id)
            {
                return BadRequest();
            }

            _uow.PizzasInOrder.Update(pizzaInOrder);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/PizzasInOrder
        [HttpPost]
        public async Task<ActionResult<PizzaInOrder>> PostPizzaInOrder(PizzaInOrder pizzaInOrder)
        {
            await _uow.PizzasInOrder.AddAsync(pizzaInOrder);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetPizzaInOrder", new { id = pizzaInOrder.Id }, pizzaInOrder);
        }

        // DELETE: api/PizzasInOrder/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PizzaInOrder>> DeletePizzaInOrder(int id)
        {
            var pizzaInOrder = await _uow.PizzasInOrder.FindAsync(id);
            if (pizzaInOrder == null)
            {
                return NotFound();
            }

            _uow.PizzasInOrder.Remove(pizzaInOrder);
            await _uow.SaveChangesAsync();

            return NoContent();
        }
    }
}
