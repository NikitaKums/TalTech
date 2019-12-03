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
    /// Controller for handling ProductInCategory objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsInCategoryController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// ProductsInCategory controller constructor.
        /// </summary>
        /// <param name="bll">Interface with services via which repositories are accessed.</param>
        public ProductsInCategoryController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/ProductsInCategory
        /// <summary>
        /// Get ProductInCategory objects associated with currently logged in user shop.
        /// </summary>
        /// <returns>Array of ProductInCategory with count of products in each.</returns>
        /// <response code="200">The array of all ProductInCategory was successfully retrieved.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.ProductInCategory>>> GetProductsInCategory()
        {
            var productInCategory = (await _bll.ProductsInCategory.AllAsyncByShopDTO(User.GetShopId())).Select(e => ProductInCategoryMapper.MapFromBLL(e)).ToList();
            return productInCategory;        
        }

        // GET: api/ProductsInCategory/5
        /// <summary>
        /// Get ProductInCategory object associated with currently logged in user shop by provided identifier.        
        /// </summary>
        /// <param name="id">The identifier by which the ProductInCategory object is being searched for.</param>
        /// <returns>ProductInCategory object with count of products in it</returns>
        /// <response code="200">The ProductInCategory was successfully retrieved.</response>
        /// <response code="404">ProductInCategory object with provided identifier was not found.</response>			
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.ProductInCategory>> GetProductInCategory(int id)
        {
            var productInCategory = await _bll.ProductsInCategory.GetAsyncByShopAndIdDTO(id, User.GetShopId());

            if (productInCategory == null)
            {
                return NotFound();
            }

            return ProductInCategoryMapper.MapFromBLL(productInCategory);
        }

        // PUT: api/ProductsInCategory/5
        /// <summary>
        /// Edit a ProductInCategory object.
        /// </summary>
        /// <param name="id">Identifier of the ProductInCategory object to be edited.</param>
        /// <param name="productInCategory">ProductInCategory object containing new data.</param>
        /// <response code="204">The ProductInCategory was successfully edited.</response>
        /// <response code="400">New data provided for ProductInCategory is not valid or provided identifier does not match ProductInCategory object identifier.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductInCategory(int id, PublicApi.v1.DTO.ProductInCategory productInCategory)
        {
            if (!ModelState.IsValid || id != productInCategory.Id)
            {
                return BadRequest();
            }

            _bll.ProductsInCategory.Update(ProductInCategoryMapper.MapFromExternal(productInCategory));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ProductsInCategory
        /// <summary>
        /// Create new ProductInCategory object
        /// </summary>
        /// <param name="productInCategory">ProductInCategory object to create</param>
        /// <response code="201">The ProductInCategory was successfully created.</response>
        /// <response code="400">The provided data via ProductInCategory object is not valid.</response>
        [HttpPost]
        public async Task<ActionResult<PublicApi.v1.DTO.ProductInCategory>> PostProductInCategory(PublicApi.v1.DTO.ProductInCategory productInCategory)
        {
            productInCategory = PublicApi.v1.Mappers.ProductInCategoryMapper
                .MapFromBLL(await _bll.ProductsInCategory.AddAsync(PublicApi.v1.Mappers.ProductInCategoryMapper.MapFromExternal(productInCategory)));

            await _bll.SaveChangesAsync();
            
            productInCategory = PublicApi.v1.Mappers.ProductInCategoryMapper.MapFromBLL(
                _bll.ProductsInCategory.GetUpdatesAfterUOWSaveChanges(PublicApi.v1.Mappers.ProductInCategoryMapper.MapFromExternal(productInCategory)));

            return CreatedAtAction("GetProductInCategory", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = productInCategory.Id}, productInCategory);
        }

        // DELETE: api/ProductsInCategory/5
        /// <summary>
        /// Delete ProductInCategory object
        /// </summary>
        /// <param name="id">Identifier of the ProductInCategory object to be deleted.</param>
        /// <response code="200">The ProductInCategory was successfully deleted.</response>
        /// <response code="404">There was no ProductInCategory object found with provided identifier.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductInCategory(int id)
        {
            var productInCategory = await _bll.ProductsInCategory.FindAsync(id);
            if (productInCategory == null)
            {
                return NotFound();
            }

            _bll.ProductsInCategory.Remove(id);
            await _bll.SaveChangesAsync();

            return Ok();
        }

    }
}
