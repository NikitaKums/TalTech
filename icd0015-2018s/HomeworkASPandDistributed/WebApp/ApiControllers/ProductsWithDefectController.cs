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
    /// Controller for handling ProductWithDefect objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsWithDefectController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// ProductsWithDefect controller constructor.
        /// </summary>
        /// <param name="bll">Interface with services via which repositories are accessed.</param>
        public ProductsWithDefectController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ProductsWithDefect
        /// <summary>
        /// Get ProductWithDefect objects associated with currently logged in user shop.
        /// </summary>
        /// <returns>Array of ProductWithDefect with count of products in each.</returns>
        /// <response code="200">The array of all ProductWithDefect was successfully retrieved.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.ProductWithDefect>>> GetProductsWithDefect()
        {
            var productWithDefect = (await _bll.ProductsWithDefect.AllAsyncByShopDTO(User.GetShopId())).Select(e => ProductWithDefectMapper.MapFromBLL(e)).ToList();
            return productWithDefect;
        }
        
        /// <summary>
        /// Get ProductInOrder objects associated with currently logged in user shop by provided order id.
        /// </summary>
        /// <param name="id">The identifier by which the ProductInOrder object is being searched for.</param>
        /// <returns>Array of ProductInOrder</returns>
        /// <response code="200">The array of all ProductInOrder was successfully retrieved.</response>
        [HttpGet("GetInfoByDefectId/{id}")]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.ProductWithDefect>>> GetInfoByDefectId(int id)
        {
            var productsWithDefect= (await _bll.ProductsWithDefect.AllAsyncByShopAndDefectIdDTO(id, User.GetShopId()))
                .Select(e => ProductWithDefectMapper.MapFromBLL(e)).ToList();
            return productsWithDefect;
        }

        // GET: api/ProductsWithDefect/5
        /// <summary>
        /// Get ProductWithDefect object associated with currently logged in user shop by provided identifier.        
        /// </summary>
        /// <param name="id">The identifier by which the ProductWithDefect object is being searched for.</param>
        /// <returns>ProductWithDefect object with count of products in it</returns>
        /// <response code="200">The ProductWithDefect was successfully retrieved.</response>
        /// <response code="404">ProductWithDefect object with provided identifier was not found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.ProductWithDefect>> GetProductWithDefect(int id)
        {
            var productWithDefect = await _bll.ProductsWithDefect.GetAsyncByShopAndIdDTO(id, User.GetShopId());

            if (productWithDefect == null)
            {
                return NotFound();
            }

            return ProductWithDefectMapper.MapFromBLL(productWithDefect);
        }

        // PUT: api/ProductsWithDefect/5
        /// <summary>
        /// Edit a ProductWithDefect object.
        /// </summary>
        /// <param name="id">Identifier of the ProductWithDefect object to be edited.</param>
        /// <param name="productWithDefect">ProductWithDefect object containing new data.</param>
        /// <response code="204">The ProductWithDefect was successfully edited.</response>
        /// <response code="400">New data provided for ProductWithDefect is not valid or provided identifier does not match ProductWithDefect object identifier.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductWithDefect(int id, PublicApi.v1.DTO.ProductWithDefect productWithDefect)
        {
            if (!ModelState.IsValid || id != productWithDefect.Id)
            {
                return BadRequest();
            }

            _bll.ProductsWithDefect.Update(ProductWithDefectMapper.MapFromExternal(productWithDefect));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/ProductsWithDefect
        /// <summary>
        /// Create new ProductWithDefect object
        /// </summary>
        /// <param name="productWithDefect">ProductWithDefect object to create</param>
        /// <response code="201">The ProductWithDefect was successfully created.</response>
        /// <response code="400">The provided data via ProductWithDefect object is not valid.</response>
        [HttpPost]
        public async Task<ActionResult<PublicApi.v1.DTO.ProductWithDefect>> PostProductWithDefect(PublicApi.v1.DTO.ProductWithDefect productWithDefect)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            productWithDefect = PublicApi.v1.Mappers.ProductWithDefectMapper
                .MapFromBLL(await _bll.ProductsWithDefect.AddAsync(PublicApi.v1.Mappers.ProductWithDefectMapper.MapFromExternal(productWithDefect)));

            await _bll.SaveChangesAsync();
            
            productWithDefect = PublicApi.v1.Mappers.ProductWithDefectMapper.MapFromBLL(
                _bll.ProductsWithDefect.GetUpdatesAfterUOWSaveChanges(PublicApi.v1.Mappers.ProductWithDefectMapper.MapFromExternal(productWithDefect)));

            return CreatedAtAction("GetProductWithDefect", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = productWithDefect.Id }, productWithDefect);
        }

        // DELETE: api/ProductsWithDefect/5
        /// <summary>
        /// Delete ProductWithDefect object
        /// </summary>
        /// <param name="id">Identifier of the ProductWithDefect object to be deleted.</param>
        /// <response code="200">The ProductWithDefect was successfully deleted.</response>
        /// <response code="404">There was no ProductWithDefect object found with provided identifier.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductWithDefect(int id)
        {
            var productWithDefect = await _bll.ProductsWithDefect.FindAsync(id);
            if (productWithDefect == null)
            {
                return NotFound();
            }

            _bll.ProductsWithDefect.Remove(id);
            await _bll.SaveChangesAsync();

            return Ok();
        }
    }
}
