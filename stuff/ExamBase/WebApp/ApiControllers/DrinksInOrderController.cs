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
    public class DrinksInOrderController : ControllerBase
    {
        
        private readonly IAppUnitOfWork _uow;
        public DrinksInOrderController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/DrinksInOrder
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrinkInOrder>>> GetDrinksInOrder(string search)
        {
            return Ok(await _uow.DrinksInOrder.AllAsyncWithSearchAPI(User.GetUserId(), search));
        }

        // GET: api/DrinksInOrder/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DAL.App.DTO.DrinkInOrder>> GetDrinkInOrder(int id)
        {
            var drinkInOrder = await _uow.DrinksInOrder.FindAsyncByIdAPI(id, User.GetUserId());

            if (drinkInOrder == null)
            {
                return NotFound();
            }

            return drinkInOrder;
        }

        // PUT: api/DrinksInOrder/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrinkInOrder(int id, DrinkInOrder drinkInOrder)
        {
            if (id != drinkInOrder.Id)
            {
                return BadRequest();
            }

            _uow.DrinksInOrder.Update(drinkInOrder);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/DrinksInOrder
        [HttpPost]
        public async Task<ActionResult<DrinkInOrder>> PostDrinkInOrder(DrinkInOrder drinkInOrder)
        {
            await _uow.DrinksInOrder.AddAsync(drinkInOrder);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetDrinkInOrder", new { id = drinkInOrder.Id }, drinkInOrder);
        }

        // DELETE: api/DrinksInOrder/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DrinkInOrder>> DeleteDrinkInOrder(int id)
        {
            var drinkInOrder = await _uow.DrinksInOrder.FindAsync(id);
            if (drinkInOrder == null)
            {
                return NotFound();
            }

            _uow.DrinksInOrder.Remove(drinkInOrder);
            await _uow.SaveChangesAsync();

            return NoContent();
        }
    }
}
