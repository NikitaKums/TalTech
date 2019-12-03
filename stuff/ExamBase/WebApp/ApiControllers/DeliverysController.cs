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
    public class DeliverysController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        public DeliverysController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }


        // GET: api/Deliverys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliverys(string search)
        {
            return Ok(await _uow.Deliverys.AllAsyncWithSearchAPI(search));
        }

        // GET: api/Deliverys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DAL.App.DTO.Delivery>> GetDelivery(int id)
        {
            var delivery = await _uow.Deliverys.FindAsyncByIdAPI(id);

            if (delivery == null)
            {
                return NotFound();
            }

            return delivery;
        }

        // PUT: api/Deliverys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDelivery(int id, Delivery delivery)
        {
            if (id != delivery.Id)
            {
                return BadRequest();
            }

            _uow.Deliverys.Update(delivery);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Deliverys
        [HttpPost]
        public async Task<ActionResult<DAL.App.DTO.Delivery>> PostDelivery(Delivery delivery)
        {
            await _uow.Deliverys.AddAsync(delivery);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetDelivery", new { id = delivery.Id }, delivery);
        }

        // DELETE: api/Deliverys/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DAL.App.DTO.Delivery>> DeleteDelivery(int id)
        {
            var delivery = await _uow.Deliverys.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }

            _uow.Deliverys.Remove(delivery);
            await _uow.SaveChangesAsync();

            return NoContent();
        }
        
    }
}
