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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        public PizzasController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }


        // GET: api/Pizzas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pizza>>> GetPizzas(string search)
        {
            return Ok(await _uow.Pizzas.AllAsyncWithSearchAPI(search));
        }

        // GET: api/Pizzas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DAL.App.DTO.Pizza>> GetPizza(int id)
        {
            var pizza = await _uow.Pizzas.FindAsyncByIdAPI(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return pizza;
        }

        // PUT: api/Pizzas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizza(int id, Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return BadRequest();
            }

            _uow.Pizzas.Update(pizza);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Pizzas
        [HttpPost]
        public async Task<ActionResult<Pizza>> PostPizza(Pizza pizza)
        {
            await _uow.Pizzas.AddAsync(pizza);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetPizza", new { id = pizza.Id }, pizza);
        }

        // DELETE: api/Pizzas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pizza>> DeletePizza(int id)
        {
            var pizza = await _uow.Pizzas.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }

            _uow.Pizzas.Remove(pizza);
            await _uow.SaveChangesAsync();

            return NoContent();
        }
    }
}
