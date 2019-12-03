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
    /// Controller for handling ProductSold objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsSoldController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// ProductsSold controller constructor.
        /// </summary>
        /// <param name="bll">Interface with services via which repositories are accessed.</param>
        public ProductsSoldController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/ProductsSold
        /// <summary>
        /// Get ProductSold objects associated with currently logged in user shop.
        /// </summary>
        /// <returns>Array of ProductSold with count of products in each.</returns>
        /// <response code="200">The array of all ProductSold was successfully retrieved.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.ProductSold>>> GetProductsSold()
        {
            var productSold = (await _bll.ProductsSold.AllAsyncByShopDTO(User.GetShopId()))
                .Select(e => ProductSoldMapper.MapFromBLL(e)).ToList();
            return productSold;
        }

        /// <summary>
        /// Get ProductSold objects associated with currently logged in user shop by provided sale id.
        /// </summary>
        /// <param name="id">The identifier by which the ProductSold object is being searched for.</param>
        /// <returns>Array of ProductSold</returns>
        /// <response code="200">The array of all ProductSold was successfully retrieved.</response>
        [HttpGet("GetInfoBySaleId/{id}")]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.ProductSold>>> GetInfoBySaleId(int id)
        {
            var productSold = (await _bll.ProductsSold.AllAsyncByShopAndSaleId(id, User.GetShopId()))
                .Select(e => ProductSoldMapper.MapFromBLL(e)).ToList();
            return productSold;
        }

        // GET: api/ProductsSold/5
        /// <summary>
        /// Get ProductSold object associated with currently logged in user shop by provided identifier.        
        /// </summary>
        /// <param name="id">The identifier by which the ProductSold object is being searched for.</param>
        /// <returns>ProductSold object with count of products in it</returns>
        /// <response code="200">The ProductSold was successfully retrieved.</response>
        /// <response code="404">ProductSold object with provided identifier was not found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.ProductSold>> GetProductSold(int id)
        {
            var productSold = await _bll.ProductsSold.GetAsyncByShopAndIdDTO(id, User.GetShopId());

            if (productSold == null)
            {
                return NotFound();
            }

            return ProductSoldMapper.MapFromBLL(productSold);
        }

        // PUT: api/ProductsSold/5
        /// <summary>
        /// Edit a ProductSold object.
        /// </summary>
        /// <param name="id">Identifier of the ProductSold object to be edited.</param>
        /// <param name="productSold">ProductSold object containing new data.</param>
        /// <response code="204">The ProductSold was successfully edited.</response>
        /// <response code="400">New data provided for ProductSold is not valid or provided identifier does not match ProductSold object identifier.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductSold(int id, PublicApi.v1.DTO.ProductSold productSold)
        {
            if (!ModelState.IsValid || id != productSold.Id)
            {
                return BadRequest();
            }

            if (await _bll.ProductsSold.EditProductInSale(id, productSold.ProductId,
                ProductSoldMapper.MapFromExternal(productSold)))
            {
                return NoContent();
            }

            return BadRequest();
        }

        // POST: api/ProductsSold
        /// <summary>
        /// Create new ProductSold object
        /// </summary>
        /// <param name="productSold">ProductSold object to create</param>
        /// <response code="201">The ProductSold was successfully created.</response>
        /// <response code="400">The provided data via ProductSold object is not valid.</response>
        [HttpPost]
        public async Task<ActionResult<ProductSold>> PostProductSold(PublicApi.v1.DTO.ProductSold productSold)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (await _bll.ProductsSold.AddProductToSale(productSold.ProductId,
                ProductSoldMapper.MapFromExternal(productSold)))
            {
                await _bll.SaveChangesAsync();

                productSold = PublicApi.v1.Mappers.ProductSoldMapper.MapFromBLL(
                    _bll.ProductsSold.GetUpdatesAfterUOWSaveChanges(
                        PublicApi.v1.Mappers.ProductSoldMapper.MapFromExternal(productSold)));

                return CreatedAtAction("GetProductSold",
                    new {version = HttpContext.GetRequestedApiVersion().ToString(), id = productSold.Id}, productSold);
            }

            return BadRequest();
        }

        // DELETE: api/ProductsSold/5
        /// <summary>
        /// Delete ProductSold object
        /// </summary>
        /// <param name="id">Identifier of the ProductSold object to be deleted.</param>
        /// <response code="200">The ProductSold was successfully deleted.</response>
        /// <response code="404">There was no ProductSold object found with provided identifier.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductSold(int id)
        {
            var productSold = await _bll.ProductsSold.FindAsync(id);
            if (productSold == null)
            {
                return NotFound();
            }

            _bll.ProductsSold.Remove(id);
            await _bll.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Delete ProductSold object restoring product quantity
        /// </summary>
        /// <param name="id">Identifier of the ProductSold object to be deleted.</param>
        /// <response code="200">The ProductSold was successfully deleted and quantity restored.</response>
        [HttpPost("DeleteWithProductRestore/{id}")]
        public async Task<ActionResult> DeleteWithProductRestore(int id)
        {
            await _bll.ProductsSold.DeleteWithRestoration(id);
            return Ok();
        }
    }
}