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
    /// Controller for handling Return objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReturnsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Returns controller constructor.
        /// </summary>
        /// <param name="bll">Interface with services via which repositories are accessed.</param>
        public ReturnsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/Returns
        /// <summary>
        /// Get Return objects associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Array of Return with count of products in each.</returns>
        /// <response code="200">The array of all Return was successfully retrieved.</response>
        /// <response code="400">PageIndex or PageSize provided were smaller than 1.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.Return>>> GetReturns(string search, int? pageIndex, int? pageSize)
        {
            if ((pageIndex != null && pageIndex < 1) || (pageSize != null && pageSize < 1))
            {
                return BadRequest();
            }
            var @return = (await _bll.Returns.GetAllWithProductsReturnedByShopAsync(User.GetShopId(), search, pageIndex, pageSize)).Select(e => ReturnMapper.MapFromBLL(e)).ToList();
            return @return;
        }
        
        /// <summary>
        /// Get Return objects count associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <returns>Array of Categories with count of products in each.</returns>
        /// <response code="200">The array of all Categories was successfully retrieved.</response>
        [HttpGet("GetDataAmount")]
        public async Task<ActionResult<int>> GetDataAmount(string search)
        {
            var @return = await _bll.Returns.CountDataAmount(User.GetShopId(), search);
            return @return;
        }
        
        /// <summary>
        /// Get ReturnIdName objects associated with currently logged in user shop.        
        /// </summary>
        /// <returns>Array ReturnIdName objects with count of products in it</returns>
        /// <response code="200">Array of ReturnIdName objects was successfully retrieved.</response>
        [HttpGet]
        [Route("ReturnIdName")]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.IdAndNameDTO.ReturnIdName>>> GetReturnIdName()
        {
            var @return = (await _bll.Returns.GetAllIdAndDescAsyncByShopDTO(User.GetShopId())).Select(e => ReturnMapper.MapFromBLL(e)).ToList();
            return @return;
        }

        // GET: api/Returns/5
        /// <summary>
        /// Get Return object associated with currently logged in user shop by provided identifier.        
        /// </summary>
        /// <param name="id">The identifier by which the Return object is being searched for.</param>
        /// <returns>Return object with count of products in it</returns>
        /// <response code="200">The Return was successfully retrieved.</response>
        /// <response code="404">Return object with provided identifier was not found.</response>			
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.Return>> GetReturn(int id)
        {   
            var @return = await _bll.Returns.FindWithProductsReturnedByIdAndShopAsync(id, User.GetShopId());

            if (@return == null)
            {
                return NotFound();
            }

            return ReturnMapper.MapFromBLL(@return);
        }

        // PUT: api/Returns/5
        /// <summary>
        /// Edit a Return object.
        /// </summary>
        /// <param name="id">Identifier of the Return object to be edited.</param>
        /// <param name="return">Return object containing new data.</param>
        /// <response code="204">The Return was successfully edited.</response>
        /// <response code="400">New data provided for Return is not valid or provided identifier does not match Return object identifier or return shop id doesn't match user shop id.</response>

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReturn(int id, PublicApi.v1.DTO.Return @return)
        {
            if (!ModelState.IsValid || id != @return.Id || @return.ShopId != User.GetShopId())
            {
                return BadRequest();
            }

            _bll.Returns.Update(ReturnMapper.MapFromExternal(@return));
            await _bll.SaveChangesAsync();

            return NoContent();


        }

        // POST: api/Returns
        /// <summary>
        /// Create new Return object
        /// </summary>
        /// <param name="return">Return object to create</param>
        /// <response code="201">The Return was successfully created.</response>
        /// <response code="400">The provided data via Return object is not valid or return shop id doesn't match user shop id.</response>
        [HttpPost]
        public async Task<ActionResult<Return>> PostReturn(PublicApi.v1.DTO.Return @return)
        {
            if (!ModelState.IsValid || @return.ShopId != User.GetShopId())
            {
                return BadRequest();
            }
            
            @return = PublicApi.v1.Mappers.ReturnMapper
                .MapFromBLL(await _bll.Returns.AddAsync(PublicApi.v1.Mappers.ReturnMapper.MapFromExternal(@return)));

            await _bll.SaveChangesAsync();
            
            @return = PublicApi.v1.Mappers.ReturnMapper.MapFromBLL(
                _bll.Returns.GetUpdatesAfterUOWSaveChanges(PublicApi.v1.Mappers.ReturnMapper.MapFromExternal(@return)));

            return CreatedAtAction("GetReturn", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = @return.Id }, @return);
        }

        // DELETE: api/Returns/5
        /// <summary>
        /// Delete Return object
        /// </summary>
        /// <param name="id">Identifier of the Return object to be deleted.</param>
        /// <response code="200">The Return was successfully deleted.</response>
        /// <response code="400">The Return object to be deleted has Product objects referenced to it.</response>
        /// <response code="404">There was no Return object found with provided identifier or return shop id doesn't match user shop id.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReturn(int id)
        {
            var @return = await _bll.Returns.FindAsync(id);
            if (@return == null || @return.ShopId != User.GetShopId())
            {
                return NotFound();
            }
            
            if (await _bll.ProductsReturned.CountProductsInReturn(id) != 0)
            {
                return NoContent();
            }

            _bll.Returns.Remove(id);
            await _bll.SaveChangesAsync();

            return Ok();
        }

    }
}
