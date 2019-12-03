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
    /// Controller for handling ProductInOrder objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsInOrderController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// ProductsInOrder controller constructor.
        /// </summary>
        /// <param name="bll">Interface with services via which repositories are accessed.</param>
        public ProductsInOrderController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ProductsInOrder
        /// <summary>
        /// Get ProductInOrder objects associated with currently logged in user shop.
        /// </summary>
        /// <returns>Array of ProductInOrder with count of products in each.</returns>
        /// <response code="200">The array of all ProductInOrder was successfully retrieved.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.ProductInOrder>>> GetProductsInOrder()
        {
            var productInOrder = (await _bll.ProductsInOrder.AllAsyncByShopDTO(User.GetShopId()))
                .Select(e => ProductInOrderMapper.MapFromBLL(e)).ToList();
            return productInOrder;
        }
        
        /// <summary>
        /// Get ProductInOrder objects associated with currently logged in user shop by provided order id.
        /// </summary>
        /// <param name="id">The identifier by which the ProductInOrder object is being searched for.</param>
        /// <returns>Array of ProductInOrder</returns>
        /// <response code="200">The array of all ProductInOrder was successfully retrieved.</response>
        [HttpGet("GetInfoByOrderId/{id}")]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.ProductInOrder>>> GetInfoByOrderId(int id)
        {
            var productInOrder = (await _bll.ProductsInOrder.AllAsyncByShopAndOrderId(id, User.GetShopId()))
                .Select(e => ProductInOrderMapper.MapFromBLL(e)).ToList();
            return productInOrder;
        }

        // GET: api/ProductsInOrder/5
        /// <summary>
        /// Get ProductInOrder object associated with currently logged in user shop by provided identifier.        
        /// </summary>
        /// <param name="id">The identifier by which the ProductInOrder object is being searched for.</param>
        /// <returns>ProductInOrder object with count of products in it</returns>
        /// <response code="200">The ProductInOrder was successfully retrieved.</response>
        /// <response code="404">ProductInOrder object with provided identifier was not found.</response>	
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.ProductInOrder>> GetProductInOrder(int id)
        {
            var productInOrder = await _bll.ProductsInOrder.GetAsyncByShopAndIdDTO(id, User.GetShopId());

            if (productInOrder == null)
            {
                return NotFound();
            }

            return ProductInOrderMapper.MapFromBLL(productInOrder);
        }

        // PUT: api/ProductsInOrder/5
        /// <summary>
        /// Edit a ProductInOrder object.
        /// </summary>
        /// <param name="id">Identifier of the ProductInOrder object to be edited.</param>
        /// <param name="productInOrder">ProductInOrder object containing new data.</param>
        /// <response code="204">The ProductInOrder was successfully edited.</response>
        /// <response code="400">New data provided for ProductInOrder is not valid or provided identifier does not match ProductInOrder object identifier.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductInOrder(int id, PublicApi.v1.DTO.ProductInOrder productInOrder)
        {
            if (!ModelState.IsValid || id != productInOrder.Id)
            {
                return BadRequest();
            }

            _bll.ProductsInOrder.Update(ProductInOrderMapper.MapFromExternal(productInOrder));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ProductsInOrder
        /// <summary>
        /// Create new ProductInOrder object
        /// </summary>
        /// <param name="productInOrder">ProductInOrder object to create</param>
        /// <response code="201">The ProductInOrder was successfully created.</response>
        /// <response code="400">The provided data via ProductInOrder object is not valid.</response>
        [HttpPost]
        public async Task<ActionResult<PublicApi.v1.DTO.ProductInOrder>> PostProductInOrder(PublicApi.v1.DTO.ProductInOrder productInOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            productInOrder = PublicApi.v1.Mappers.ProductInOrderMapper
                .MapFromBLL(await _bll.ProductsInOrder.AddAsync(PublicApi.v1.Mappers.ProductInOrderMapper.MapFromExternal(productInOrder)));

            await _bll.SaveChangesAsync();
            
            productInOrder = PublicApi.v1.Mappers.ProductInOrderMapper.MapFromBLL(
                _bll.ProductsInOrder.GetUpdatesAfterUOWSaveChanges(PublicApi.v1.Mappers.ProductInOrderMapper.MapFromExternal(productInOrder)));

            return CreatedAtAction("GetProductInOrder", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = productInOrder.Id }, productInOrder);
        }

        // DELETE: api/ProductsInOrder/5
        /// <summary>
        /// Delete ProductInOrder object
        /// </summary>
        /// <param name="id">Identifier of the ProductInOrder object to be deleted.</param>
        /// <response code="200">The ProductInOrder was successfully deleted.</response>
        /// <response code="404">There was no ProductInOrder object found with provided identifier.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductInOrder(int id)
        {
            var productInOrder = await _bll.ProductsInOrder.FindAsync(id);
            if (productInOrder == null)
            {
                return NotFound();
            }

            _bll.ProductsInOrder.Remove(id);
            await _bll.SaveChangesAsync();

            return Ok();
        }
        
        /// <summary>
        /// Process received order -> increase product quantity according to order
        /// </summary>
        /// <param name="id">ProductInOrder object to process</param>
        /// <response code="200">The ProductInOrder was successfully processed.</response>
        [HttpPost("ProductInOrderReceived/{id}")]
        public async Task<IActionResult>  ProductInOrderReceived(int id)
        {
            await _bll.ProductsInOrder.ProductInOrderReceived(id);
            return Ok();
        }
    }
}