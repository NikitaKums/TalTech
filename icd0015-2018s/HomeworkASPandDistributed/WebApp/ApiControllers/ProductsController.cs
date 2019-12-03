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
    /// Controller for handling Product objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Products controller constructor.
        /// </summary>
        /// <param name="bll">Interface with services via which repositories are accessed.</param>
        public ProductsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Products
        /// <summary>
        /// Get Product objects associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Array of Product with count of orders/sale amounts/returns/defects in each and Comment id/title and Category id/name arrays.</returns>
        /// <response code="200">The array of all Products was successfully retrieved.</response>
        /// <response code="400">PageIndex or PageSize provided were smaller than 1.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.ProductWithDataStuff>>> GetProducts(string search, int? pageIndex, int? pageSize)
        {
            if ((pageIndex != null && pageIndex < 1) || (pageSize != null && pageSize < 1))
            {
                return BadRequest();
            }
            var product = (await _bll.Products.AllAsyncByShopDTO(User.GetShopId(), search, pageIndex, pageSize)).Select(e => ProductMapper.MapFromBLL(e)).ToList();
            return product;
        }
        
        /// <summary>
        /// Get Product objects count associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <returns>Array of Categories with count of products in each.</returns>
        /// <response code="200">The array of all Categories was successfully retrieved.</response>
        [HttpGet("GetDataAmount")]
        public async Task<ActionResult<int>> GetDataAmount(string search)
        {
            var product = await _bll.Products.CountDataAmount(User.GetShopId(), search);
            return product;
        }
        
        /// <summary>
        /// Get ProductIdName objects associated with currently logged in user shop.        
        /// </summary>
        /// <returns>ProductIdName object with count of products in it</returns>
        /// <response code="200">Array of ProductIdName objects was successfully retrieved.</response>
        /// <response code="404">Not a single ProductIdName object with provided identifier was not found.</response>			
        [HttpGet]
        [Route("GetIdAndName")]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.IdAndNameDTO.ProductIdName>>> GetIdAndName()
        {
            var products = await _bll.Products.GetProductIdNameByShopInInventoryDTO(User.GetShopId());
            
            if (products == null)
            {
                return NotFound();
            }
            
            return products.Select(e => ProductMapper.MapFromBLL(e)).ToList();
        }
        
        /// <summary>
        /// Get ProductIdName objects associated with currently logged in user shop and category id.        
        /// </summary>
        /// <returns>ProductIdName object with count of products in it</returns>
        /// <param name="id">The identifier of category by which the ProductIdName object is being searched for.</param>
        /// <response code="200">Array of ProductIdName objects was successfully retrieved.</response>
        /// <response code="404">Not a single ProductIdName object with provided identifier was not found.</response>			
        [HttpGet]
        [Route("GetIdAndNameByCategory/{id}")]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.IdAndNameDTO.ProductIdName>>> GetIdAndNameByCategory(int id)
        {
            var products = await _bll.ProductsInCategory.AllAsyncByCategoryIdAndShopId(id, User.GetShopId());
            
            if (products == null)
            {
                return NotFound();
            }
            
            return products.Select(e => ProductMapper.MapFromBLL(e)).ToList();
        }
        
        /// <summary>
        /// Get ProductIdName objects associated with currently logged in user shop and order id.        
        /// </summary>
        /// <returns>ProductIdName object with count of products in it</returns>
        /// <param name="id">The identifier of category by which the ProductIdName object is being searched for.</param>
        /// <response code="200">Array of ProductIdName objects was successfully retrieved.</response>
        /// <response code="404">Not a single ProductIdName object with provided identifier was not found.</response>			
        [HttpGet]
        [Route("GetIdAndNameByDefect/{id}")]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.IdAndNameDTO.ProductIdName>>> GetIdAndNameByDefect(int id)
        {
            var products = await _bll.ProductsWithDefect.AllAsyncByDefectIdAndShopId(id, User.GetShopId());
            
            if (products == null)
            {
                return NotFound();
            }
            
            return products.Select(e => ProductMapper.MapFromBLL(e)).ToList();
        }

        // GET: api/Products/5
        /// <summary>
        /// Get ProductIdName object associated with currently logged in user shop by provided identifier.        
        /// </summary>
        /// <param name="id">The identifier by which the ProductIdName object is being searched for.</param>
        /// <returns>ProductIdName object with count of products in it</returns>
        /// <response code="200">The ProductIdName was successfully retrieved.</response>
        /// <response code="404">ProductIdName object with provided identifier was not found.</response>			
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.IdAndNameDTO.ProductIdName>> GetProduct(int id)
        {
            var product = await _bll.Products.FindByShopAndId(id, User.GetShopId());

            if (product == null)
            {
                return NotFound();
            }

            return ProductMapper.MapFromBLL(product);
        }

        // PUT: api/Products/5
        /// <summary>
        /// Edit a Product object.
        /// </summary>
        /// <param name="id">Identifier of the Product object to be edited.</param>
        /// <param name="product">Product object containing new data.</param>
        /// <response code="204">The Product was successfully edited.</response>
        /// <response code="400">New data provided for Product is not valid or provided identifier does not match Product object identifier or product shop id doesn't match user shop id.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, PublicApi.v1.DTO.Product product)
        {
            if (!ModelState.IsValid || id != product.Id || product.ShopId != User.GetShopId())
            {
                return BadRequest();
            }

            _bll.Products.Update(ProductMapper.MapFromExternal(product));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Products
        /// <summary>
        /// Create new Product object
        /// </summary>
        /// <param name="product">Product object to create</param>
        /// <response code="201">The Product was successfully created.</response>
        /// <response code="400">The provided data via Product object is not valid or product shop id doesn't match user shop id.</response>
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(PublicApi.v1.DTO.Product product)
        {
            if (!ModelState.IsValid || product.ShopId != User.GetShopId())
            {
                return BadRequest();
            }

            product = PublicApi.v1.Mappers.ProductMapper
                .MapFromBLL(await _bll.Products.AddAsync(PublicApi.v1.Mappers.ProductMapper.MapFromExternal(product)));

            await _bll.SaveChangesAsync();
            
            product = PublicApi.v1.Mappers.ProductMapper.MapFromBLL(
                _bll.Products.GetUpdatesAfterUOWSaveChanges(PublicApi.v1.Mappers.ProductMapper.MapFromExternal(product)));
            
            return CreatedAtAction("GetProduct", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = product.Id }, product);
        }

        // DELETE: api/Products/5
        /// <summary>
        /// Delete Product object
        /// </summary>
        /// <param name="id">Identifier of the Product object to be deleted.</param>
        /// <response code="200">The Product was successfully deleted.</response>
        /// <response code="404">There was no Product object found with provided identifier or product shop id doesn't match user shop id.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _bll.Products.FindAsync(id);
            if (product == null || product.ShopId != User.GetShopId())
            {
                return NotFound();
            }

            _bll.Products.Remove(id);
            await _bll.SaveChangesAsync();

            return Ok();
        }

    }
}
