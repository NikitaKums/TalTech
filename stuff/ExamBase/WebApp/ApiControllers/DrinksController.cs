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
    public class DrinksController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        public DrinksController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }


        // GET: api/Drinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drink>>> GetDrinks(string search)
        {
            return Ok(await _uow.Drinks.AllAsyncWithSearchAPI(search));
        }

        // GET: api/Drinks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DAL.App.DTO.Drink>> GetDrink(int id)
        {
            var drink = await _uow.Drinks.FindAsyncByIdAPI(id);

            if (drink == null)
            {
                return NotFound();
            }

            return drink;
        }

        // PUT: api/Drinks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrink(int id, Drink drink)
        {
            if (id != drink.Id)
            {
                return BadRequest();
            }
            
            _uow.Drinks.Update(drink);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Drinks
        [HttpPost]
        public async Task<ActionResult<DAL.App.DTO.Drink>> PostDrink(Drink drink)
        {
            await _uow.Drinks.AddAsync(drink);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetDrink", new { id = drink.Id }, drink);
        }

        // DELETE: api/Drinks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Drink>> DeleteDrink(int id)
        {
            var drink = await _uow.Drinks.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }

            _uow.Drinks.Remove(drink);
            await _uow.SaveChangesAsync();

            return NoContent();
        }
    }
}
