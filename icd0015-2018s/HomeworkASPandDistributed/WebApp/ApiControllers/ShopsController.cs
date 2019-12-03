using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Domain;
using ee.itcollege.nikita.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Controller for handling Shop objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShopsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Shops controller constructor.
        /// </summary>
        /// <param name="bll">Interface with services via which repositories are accessed.</param>
        public ShopsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/Shops
        /// <summary>
        /// Get all Shop objects.
        /// </summary>
        /// <returns>Array of Shop with count of products/orders/users/defects/returns in each.</returns>
        /// <response code="200">The array of all Shop was successfully retrieved.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.Shop>>> GetShops()
        {
            var shop = (await _bll.Shops.GetAllWithCountsAsync()).Select(e => ShopMapper.MapFromBLL(e)).ToList();
            
            return shop;
        }

        /// <summary>
        /// Get Shop object associated with currently logged in user.        
        /// </summary>
        /// <returns>Shop object with count of products/orders/users/defects/returns in each.</returns>
        /// <response code="200">The Shop was successfully retrieved.</response>
        /// <response code="404">Shop object with provided identifier was not found.</response>			
        [HttpGet]
        [Route("GetSingle")]
        public async Task<ActionResult<PublicApi.v1.DTO.Shop>> GetSingle()
        {
            var user = await _bll.AppUsers.FindAsync(User.GetUserId());

            if (user.ShopId == null)
            {
                return NotFound();
            }

            var shop = await _bll.Shops.GetShopByShopId(user.ShopId);
            if (shop == null)
            {
                return NotFound();
            }

            return ShopMapper.MapFromBLL(shop);
        }

        // GET: api/Shops/5
        /// <summary>
        /// Get Shop object by provided identifier.        
        /// </summary>
        /// <param name="id">The identifier by which the Shop object is being searched for.</param>
        /// <returns>Shop object with count of products/orders/users/defects/returns in each.</returns>
        /// <response code="200">The Shop was successfully retrieved.</response>
        /// <response code="404">Shop object with provided identifier was not found.</response>			
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.Shop>> GetShop(int id)
        {
            var shop = await _bll.Shops.GetShopByShopId(id);

            if (shop == null)
            {
                return NotFound();
            }

            return ShopMapper.MapFromBLL(shop);
        }

        // PUT: api/Shops/5
        /// <summary>
        /// Edit a Shop object.
        /// </summary>
        /// <param name="id">Identifier of the Shop object to be edited.</param>
        /// <param name="shop">Shop object containing new data.</param>
        /// <response code="204">The Shop was successfully edited.</response>
        /// <response code="400">New data provided for Shop is not valid or provided identifier does not match Shop object identifier.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShop(int id, PublicApi.v1.DTO.Shop shop)
        {
            if (!ModelState.IsValid || id != shop.Id)
            {
                return BadRequest();
            }

            _bll.Shops.Update(ShopMapper.MapFromExternal(shop));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Shops
        /// <summary>
        /// Create new Shop object
        /// </summary>
        /// <param name="shop">Shop object to create</param>
        /// <response code="201">The Shop was successfully created.</response>
        /// <response code="400">The provided data via Shop object is not valid.</response>
        [HttpPost]
        public async Task<ActionResult<Shop>> PostShop(PublicApi.v1.DTO.Shop shop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            shop = PublicApi.v1.Mappers.ShopMapper
                .MapFromBLL(await _bll.Shops.AddAsync(PublicApi.v1.Mappers.ShopMapper.MapFromExternal(shop)));

            await _bll.SaveChangesAsync();
            
            shop = PublicApi.v1.Mappers.ShopMapper.MapFromBLL(
                _bll.Shops.GetUpdatesAfterUOWSaveChanges(PublicApi.v1.Mappers.ShopMapper.MapFromExternal(shop)));

            return CreatedAtAction("GetShop", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = shop.Id }, shop);
        }

        // DELETE: api/Shops/5
        /// <summary>
        /// Delete Shop object
        /// </summary>
        /// <param name="id">Identifier of the Shop object to be deleted.</param>
        /// <response code="200">The Shop was successfully deleted.</response>
        /// <response code="400">The Shop object to be deleted has one or more Order/Defect/Inventory/AppUser/Return/Product objects referenced to it.</response>
        /// <response code="404">There was no Shop object found with provided identifier.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShop(int id)
        {
            var shop = await _bll.Shops.GetShopByShopId(id);
            if (shop == null)
            {
                return NotFound();
            }

            if (shop.OrdersCount != 0 || shop.DefectsCount != 0 || shop.InventoryId != 0 ||
                shop.AppUsersCount != 0 || shop.ReturnsCount != 0 || shop.ProductsCount != 0)
            {
                return BadRequest();
            }

            _bll.Shops.Remove(id);
            await _bll.SaveChangesAsync();

            return Ok();
        }
    }
}
