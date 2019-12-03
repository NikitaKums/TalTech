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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ToppingsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        public ToppingsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }


        // GET: api/Toppings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Topping>>> GetToppings(string search)
        {
            return Ok(await _uow.Toppings.AllAsyncWithSearchAPI(search));
        }

        // GET: api/Toppings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DAL.App.DTO.Topping>> GetTopping(int id)
        {
            var topping = await _uow.Toppings.FindAsyncByIdAPI(id);

            if (topping == null)
            {
                return NotFound();
            }

            return topping;
        }

        // PUT: api/Toppings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopping(int id, Topping topping)
        {
            if (id != topping.Id)
            {
                return BadRequest();
            }

            _uow.Toppings.Update(topping);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Toppings
        [HttpPost]
        public async Task<ActionResult<Topping>> PostTopping(Topping topping)
        {
            await _uow.Toppings.AddAsync(topping);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetTopping", new { id = topping.Id }, topping);
        }

        // DELETE: api/Toppings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Topping>> DeleteTopping(int id)
        {
            var topping = await _uow.Toppings.FindAsync(id);
            if (topping == null)
            {
                return NotFound();
            }

            _uow.Toppings.Remove(topping);
            await _uow.SaveChangesAsync();

            return NoContent();
        }
    }
}
