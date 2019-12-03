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
    public class ToppingsOnPizzaController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        public ToppingsOnPizzaController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }


        // GET: api/ToppingsOnPizza
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToppingOnPizza>>> GetToppingsOnPizzas(string search)
        {
            return Ok(await _uow.ToppingsOnPizza.AllAsyncWithSearchAPI(User.GetUserId(), search));
        }

        // GET: api/ToppingsOnPizza/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DAL.App.DTO.ToppingOnPizza>> GetToppingOnPizza(int id)
        {
            var toppingOnPizza = await _uow.ToppingsOnPizza.FindAsyncByIdAPI(id, User.GetUserId());

            if (toppingOnPizza == null)
            {
                return NotFound();
            }

            return toppingOnPizza;
        }

        // PUT: api/ToppingsOnPizza/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToppingOnPizza(int id, ToppingOnPizza toppingOnPizza)
        {
            if (id != toppingOnPizza.Id)
            {
                return BadRequest();
            }

            _uow.ToppingsOnPizza.Update(toppingOnPizza);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ToppingsOnPizza
        [HttpPost]
        public async Task<ActionResult<ToppingOnPizza>> PostToppingOnPizza(ToppingOnPizza toppingOnPizza)
        {
            await _uow.ToppingsOnPizza.AddAsync(toppingOnPizza);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetToppingOnPizza", new { id = toppingOnPizza.Id }, toppingOnPizza);
        }

        // DELETE: api/ToppingsOnPizza/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ToppingOnPizza>> DeleteToppingOnPizza(int id)
        {
            var toppingOnPizza = await _uow.ToppingsOnPizza.FindAsync(id);
            if (toppingOnPizza == null)
            {
                return NotFound();
            }

            _uow.ToppingsOnPizza.Remove(toppingOnPizza);
            await _uow.SaveChangesAsync();

            return NoContent();
        }
    }
}
