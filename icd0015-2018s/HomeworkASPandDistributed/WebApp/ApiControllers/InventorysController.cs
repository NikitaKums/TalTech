using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using ee.itcollege.nikita.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Controller for handling Inventory objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InventorysController : ControllerBase
    {
        
        private readonly IAppBLL _bll;

        /// <summary>
        /// Inventories controller constructor.
        /// </summary>
        /// <param name="bll">Interface with services via which repositories are accessed.</param>

        public InventorysController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Inventorys
        /// <summary>
        /// Get Inventory objects associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Array of Inventories with count of products in each.</returns>
        /// <response code="200">The array of all Inventories was successfully retrieved.</response>
        /// <response code="400">PageIndex or PageSize provided were smaller than 1.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.InventoryWithProductCount>>> GetInventory(string search, int? pageIndex, int? pageSize)
        {
            if ((pageIndex != null && pageIndex < 1) || (pageSize != null && pageSize < 1))
            {
                return BadRequest();
            }
            var inventory = (await _bll.Inventories.GetByShopAsync(User.GetShopId(), search, pageIndex, pageSize)).Select(e => InventoryMapper.MapFromBLL(e)).ToList();
            return inventory;
        }
        
        /// <summary>
        /// Get Inventory objects count associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <returns>Array of Categories with count of products in each.</returns>
        /// <response code="200">The array of all Categories was successfully retrieved.</response>
        [HttpGet("GetDataAmount")]
        public async Task<ActionResult<int>> GetDataAmount(string search)
        {
            var inventory = await _bll.Inventories.CountDataAmount(User.GetShopId(), search);
            return inventory;
        }

        // GET: api/Inventorys/5
        /// <summary>
        /// Get Inventory object associated with currently logged in user shop by provided identifier.
        /// </summary>
        /// <param name="id">The identifier by which the Inventory object is being searched for.</param>
        /// <returns>Inventory object with count of products in it</returns>
        /// <response code="200">The Inventory was successfully retrieved.</response>
        /// <response code="404">Inventory object with provided identifier was not found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.InventoryWithProductCount>> GetInventory(int id)
        {
            var inventory = await _bll.Inventories.FindByShopAsyncAndIdAsync(id, User.GetShopId());

            if (inventory == null)
            {
                return NotFound();
            }

            return InventoryMapper.MapFromBLL(inventory);
        }

        // PUT: api/Inventorys/5
        /// <summary>
        /// Edit a Inventory object.
        /// </summary>
        /// <param name="id">Identifier of the Inventory object to be edited.</param>
        /// <param name="inventory">Inventory object containing new data.</param>
        /// <response code="204">The Inventory was successfully edited.</response>
        /// <response code="400">New data provided for Inventory is not valid or provided identifier does not match Inventory object identifier or inventory shop id doesn't match user shop id.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory(int id, PublicApi.v1.DTO.Inventory inventory)
        {
            if (!ModelState.IsValid || id != inventory.Id || inventory.ShopId != User.GetShopId())
            {
                return BadRequest();
            }

            _bll.Inventories.Update(InventoryMapper.MapFromExternal(inventory));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Inventorys
        /// <summary>
        /// Create new Inventory object
        /// </summary>
        /// <param name="inventory">Inventory object to create</param>
        /// <response code="201">The Inventory was successfully created.</response>
        /// <response code="400">The provided data via Inventory object is not valid or inventory shop id doesn't match user shop id.</response>
        [HttpPost]
        public async Task<ActionResult<PublicApi.v1.DTO.InventoryWithProductCount>> PostInventory(PublicApi.v1.DTO.Inventory inventory)
        {
            if (!ModelState.IsValid || inventory.ShopId != User.GetShopId())
            {
                return BadRequest();
            }
            
            inventory = PublicApi.v1.Mappers.InventoryMapper
                .MapFromBLL(await _bll.Inventories.AddAsync(PublicApi.v1.Mappers.InventoryMapper.MapFromExternal(inventory)));

            await _bll.SaveChangesAsync();
            
            inventory = PublicApi.v1.Mappers.InventoryMapper.MapFromBLL(
                _bll.Inventories.GetUpdatesAfterUOWSaveChanges(PublicApi.v1.Mappers.InventoryMapper.MapFromExternal(inventory)));

            return CreatedAtAction("GetInventory", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = inventory.Id }, inventory);
        }

        // DELETE: api/Inventorys/5
        /// <summary>
        /// Delete Inventory object
        /// </summary>
        /// <param name="id">Identifier of the Inventory object to be deleted.</param>
        /// <response code="200">The Inventory was successfully deleted.</response>
        /// <response code="400">The Inventory object to be deleted has Product objects referenced to it.</response>
        /// <response code="404">There was no Inventory object found with provided identifier or inventory shop id doesn't match user shop id.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInventory(int id)
        {
            var inventory = await _bll.Inventories.FindAsync(id);
            if (inventory == null || inventory.ShopId != User.GetShopId())
            {
                return NotFound();
            }

            if (!await _bll.Inventories.IsInventoryEmpty(id))
            {
                return NoContent();
            }

            _bll.Inventories.Remove(id);
            await _bll.SaveChangesAsync();

            return Ok();
        }
    }
}
