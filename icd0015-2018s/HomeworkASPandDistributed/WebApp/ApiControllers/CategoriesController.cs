using System;
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
    /// Controller for handling Category objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Categories controller constructor.
        /// </summary>
        /// <param name="bll">Interface with services via which repositories are accessed.</param>
        public CategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Categories
        /// <summary>
        /// Get Category objects associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Array of Categories with count of products in each.</returns>
        /// <response code="200">The array of all Categories was successfully retrieved.</response>
        /// <response code="400">PageIndex or PageSize provided were smaller than 1.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.CategoryWithProductCount>>> GetCategories(string search, int? pageIndex, int? pageSize)
        {
            if ((pageIndex != null && pageIndex < 1) || (pageSize != null && pageSize < 1))
            {
                return BadRequest();
            }
            var category = (await _bll.Categories.GetAllWithProductCountForShopAsync(User.GetShopId(), search, pageIndex, pageSize))
                .Select(e => CategoryMapper.MapFromBLL(e)).ToList();
            return category;
        }
        
        // GET: api/Categories
        /// <summary>
        /// Get Category objects count associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <returns>Array of Categories with count of products in each.</returns>
        /// <response code="200">The array of all Categories was successfully retrieved.</response>
        [HttpGet("GetDataAmount")]
        public async Task<ActionResult<int>> GetDataAmount(string search)
        {
            var category = await _bll.Categories.CountDataAmount(User.GetShopId(), search);
            return category;
        }

        // GET: api/Categories/5
        /// <summary>
        /// Get Category object associated with currently logged in user by provided identifier.
        /// </summary>
        /// <param name="id">The identifier by which the Category object is being searched for.</param>
        /// <returns>Category object with count of products in it</returns>
        /// <response code="200">The Category was successfully retrieved.</response>
        /// <response code="404">Category object with provided identifier was not found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.CategoryWithProductCount>> GetCategory(int id)
        {
            var category = await _bll.Categories.GetByIndexAndShop(id, User.GetShopId());

            if (category == null)
            {
                return NotFound();
            }

            return CategoryMapper.MapFromBLL(category);
        }

        // PUT: api/Categories/5
        /// <summary>
        /// Edit a Category object.
        /// </summary>
        /// <param name="id">Identifier of the Category object to be edited.</param>
        /// <param name="category">Category object containing new data.</param>
        /// <response code="204">The Category was successfully edited.</response>
        /// <response code="400">New data provided for Category is not valid or provided identifier does not match Category object identifier or the category shop id didn't match user shop id.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, PublicApi.v1.DTO.Category category)
        {
            if (!ModelState.IsValid || id != category.Id || category.ShopId != User.GetShopId())
            {
                return BadRequest();
            }

            _bll.Categories.Update(CategoryMapper.MapFromExternal(category));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Categories
        /// <summary>
        /// Create new Category object
        /// </summary>
        /// <param name="category">Category object to create</param>
        /// <response code="201">The Category was successfully created.</response>
        /// <response code="400">The provided data via Category object is not valid or the category shop id didn't match user shop id.</response>
        [HttpPost]
        public async Task<ActionResult<PublicApi.v1.DTO.Category>> PostCategory(PublicApi.v1.DTO.Category category)
        {
            if (!ModelState.IsValid || category.ShopId != User.GetShopId())
            {
                return BadRequest();
            }

            category = PublicApi.v1.Mappers.CategoryMapper
                .MapFromBLL(
                    await _bll.Categories.AddAsync(PublicApi.v1.Mappers.CategoryMapper.MapFromExternal(category)));

            await _bll.SaveChangesAsync();

            category = PublicApi.v1.Mappers.CategoryMapper.MapFromBLL(
                _bll.Categories.GetUpdatesAfterUOWSaveChanges(
                    PublicApi.v1.Mappers.CategoryMapper.MapFromExternal(category)));

            return CreatedAtAction("GetCategory", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = category.Id}, category);
        }

        // DELETE: api/Categories/5
        /// <summary>
        /// Delete Category object
        /// </summary>
        /// <param name="id">Identifier of the Category object to be deleted.</param>
        /// <response code="200">The Category was successfully deleted.</response>
        /// <response code="404">There was no Category object found with provided identifier or the category shop id didn't match user shop id.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _bll.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            if (category.ShopId != User.GetShopId())
            {
                return NotFound();
            }
            _bll.Categories.Remove(id);
            await _bll.SaveChangesAsync();

            return Ok();
        }
    }
}