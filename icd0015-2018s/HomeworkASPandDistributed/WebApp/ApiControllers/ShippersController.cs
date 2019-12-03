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
    /// Controller for handling Shipper objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShippersController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Shippers controller constructor.
        /// </summary>
        /// <param name="bll">Interface with services via which repositories are accessed.</param>
        public ShippersController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/Shippers
        /// <summary>
        /// Get ShipperWithOrderCount objects associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Array of ShipperWithOrderCounts with count of orders in each.</returns>
        /// <response code="200">The array of all ShipperWithOrderCounts was successfully retrieved.</response>
        /// <response code="400">PageIndex or PageSize provided were smaller than 1.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.ShipperWithOrderCount>>> GetShippers(string search, int? pageIndex, int? pageSize)
        {
            if ((pageIndex != null && pageIndex < 1) || (pageSize != null && pageSize < 1))
            {
                return BadRequest();
            }
            var shipper = (await _bll.Shippers.GetAllWithOrdersCountByShopAsync(User.GetShopId(), search, pageIndex, pageSize)).Select(e => ShipperMapper.MapFromBLL(e)).ToList();
            return shipper;
        }
        
        /// <summary>
        /// Get Shipper objects count associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <returns>Array of Categories with count of products in each.</returns>
        /// <response code="200">The array of all Categories was successfully retrieved.</response>
        [HttpGet("GetDataAmount")]
        public async Task<ActionResult<int>> GetDataAmount(string search)
        {
            var shipper = await _bll.Shippers.CountDataAmount(User.GetShopId(), search);
            return shipper;
        }

        // GET: api/Shippers/5
        /// <summary>
        /// Get ShipperWithOrderCount object associated with currently logged in user shop by provided identifier.        
        /// </summary>
        /// <param name="id">The identifier by which the ShipperWithOrderCount object is being searched for.</param>
        /// <returns>ShipperWithOrderCount object with count of products in it</returns>
        /// <response code="200">The ShipperWithOrderCount was successfully retrieved.</response>
        /// <response code="404">ShipperWithOrderCount object with provided identifier was not found.</response>			
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.ShipperWithOrderCount>> GetShipper(int id)
        {
            var shipper = await _bll.Shippers.FindByIdAsyncDTO(id);

            if (shipper == null)
            {
                return NotFound();
            }

            return ShipperMapper.MapFromBLL(shipper);
        }

        // PUT: api/Shippers/5
        /// <summary>
        /// Edit a Shipper object.
        /// </summary>
        /// <param name="id">Identifier of the Shipper object to be edited.</param>
        /// <param name="shipper">Shipper object containing new data.</param>
        /// <response code="204">The Shipper was successfully edited.</response>
        /// <response code="400">New data provided for Shipper is not valid or provided identifier does not match Shipper object identifier.</response>

        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipper(int id, PublicApi.v1.DTO.Shipper shipper)
        {
            if (!ModelState.IsValid || id != shipper.Id)
            {
                return BadRequest();
            }

            _bll.Shippers.Update(ShipperMapper.MapFromExternal(shipper));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Shippers
        /// <summary>
        /// Create new Shipper object
        /// </summary>
        /// <param name="shipper">Shipper object to create</param>
        /// <response code="201">The Shipper was successfully created.</response>
        /// <response code="400">The provided data via Shipper object is not valid.</response>
        [HttpPost]
        public async Task<ActionResult<PublicApi.v1.DTO.Shipper>> PostShipper(PublicApi.v1.DTO.Shipper shipper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            shipper = PublicApi.v1.Mappers.ShipperMapper
                .MapFromBLL(await _bll.Shippers.AddAsync(PublicApi.v1.Mappers.ShipperMapper.MapFromExternal(shipper)));

            await _bll.SaveChangesAsync();
            
            shipper = PublicApi.v1.Mappers.ShipperMapper.MapFromBLL(
                _bll.Shippers.GetUpdatesAfterUOWSaveChanges(PublicApi.v1.Mappers.ShipperMapper.MapFromExternal(shipper)));


            return CreatedAtAction("GetShipper", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = shipper.Id }, shipper);
        }

        // DELETE: api/Shippers/5
        /// <summary>
        /// Delete Shipper object
        /// </summary>
        /// <param name="id">Identifier of the Shipper object to be deleted.</param>
        /// <response code="200">The Shipper was successfully deleted.</response>
        /// <response code="404">There was no Shipper object found with provided identifier.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteShipper(int id)
        {
            var shipper = await _bll.Shippers.FindAsync(id);
            if (shipper == null)
            {
                return NotFound();
            }

            _bll.Shippers.Remove(id);
            await _bll.SaveChangesAsync();

            return Ok();
        }
    }
}
