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
    /// Controller for handling ProductReturned objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsReturnedController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// ProductsReturned controller constructor.
        /// </summary>
        /// <param name="bll">Interface with services via which repositories are accessed.</param>
        public ProductsReturnedController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ProductsReturned
        /// <summary>
        /// Get ProductReturned objects associated with currently logged in user shop.
        /// </summary>
        /// <returns>Array of ProductReturned with count of products in each.</returns>
        /// <response code="200">The array of all ProductReturned was successfully retrieved.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.ProductReturned>>> GetProductsReturned()
        {
            var productReturned = (await _bll.ProductsReturned.AllAsyncByShopDTO(User.GetShopId())).Select(e => ProductReturnedMapper.MapFromBLL(e)).ToList();
            return productReturned;
        }
        
        /// <summary>
        /// Get ProductReturned objects associated with currently logged in user shop and given return id.
        /// </summary>
        /// <param name="id">The identifier by which the ProductReturned object is being searched for.</param>
        /// <returns>Array of ProductReturned.</returns>
        /// <response code="200">The array of all ProductReturned was successfully retrieved.</response>
        [HttpGet("GetInfoByReturnId/{id}")]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.ProductReturned>>> GetInfoByReturnId(int id)
        {
            var productReturned = (await _bll.ProductsReturned.AllAsyncByShopAndIdDTO(id, User.GetShopId())).Select(e => ProductReturnedMapper.MapFromBLL(e)).ToList();
            return productReturned;
        }

        // GET: api/ProductsReturned/5
        /// <summary>
        /// Get ProductReturned object associated with currently logged in user shop by provided identifier.        
        /// </summary>
        /// <param name="id">The identifier by which the ProductReturned object is being searched for.</param>
        /// <returns>ProductReturned object with count of products in it</returns>
        /// <response code="200">The ProductReturned was successfully retrieved.</response>
        /// <response code="404">ProductReturned object with provided identifier was not found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.ProductReturned>> GetProductReturned(int id)
        {
            var productReturned = await _bll.ProductsReturned.GetAsyncByShopAndIdDTO(id, User.GetShopId());

            if (productReturned == null)
            {
                return NotFound();
            }

            return ProductReturnedMapper.MapFromBLL(productReturned);
        }

        // PUT: api/ProductsReturned/5
        /// <summary>
        /// Edit a ProductReturned object.
        /// </summary>
        /// <param name="id">Identifier of the ProductReturned object to be edited.</param>
        /// <param name="productReturned">ProductReturned object containing new data.</param>
        /// <response code="204">The ProductReturned was successfully edited.</response>
        /// <response code="400">New data provided for ProductReturned is not valid or provided identifier does not match ProductReturned object identifier.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductReturned(int id, PublicApi.v1.DTO.ProductReturned productReturned)
        {
            if (!ModelState.IsValid || id != productReturned.Id)
            {
                return BadRequest();
            }

            _bll.ProductsReturned.Update(ProductReturnedMapper.MapFromExternal(productReturned));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/ProductsReturned
        /// <summary>
        /// Create new ProductReturned object
        /// </summary>
        /// <param name="productReturned">ProductReturned object to create</param>
        /// <response code="201">The ProductReturned was successfully created.</response>
        /// <response code="400">The provided data via ProductReturned object is not valid.</response>
        [HttpPost]
        public async Task<ActionResult<PublicApi.v1.DTO.ProductReturned>> PostProductReturned(PublicApi.v1.DTO.ProductReturned productReturned)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            productReturned = PublicApi.v1.Mappers.ProductReturnedMapper
                .MapFromBLL(await _bll.ProductsReturned.AddAsync(PublicApi.v1.Mappers.ProductReturnedMapper.MapFromExternal(productReturned)));

            await _bll.SaveChangesAsync();
            
            productReturned = PublicApi.v1.Mappers.ProductReturnedMapper.MapFromBLL(
                _bll.ProductsReturned.GetUpdatesAfterUOWSaveChanges(PublicApi.v1.Mappers.ProductReturnedMapper.MapFromExternal(productReturned)));

            return CreatedAtAction("GetProductReturned", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = productReturned.Id }, productReturned);
        }

        // DELETE: api/ProductsReturned/5
        /// <summary>
        /// Delete ProductReturned object
        /// </summary>
        /// <param name="id">Identifier of the ProductReturned object to be deleted.</param>
        /// <response code="200">The ProductReturned was successfully deleted.</response>
        /// <response code="404">There was no ProductReturned object found with provided identifier.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductReturned(int id)
        {
            var productReturned = await _bll.ProductsReturned.FindAsync(id);
            if (productReturned == null)
            {
                return NotFound();
            }

            _bll.ProductsReturned.Remove(id);
            await _bll.SaveChangesAsync();

            return Ok();
        }

    }
}
