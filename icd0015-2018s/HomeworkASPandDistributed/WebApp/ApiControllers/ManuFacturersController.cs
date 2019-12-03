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
    /// Controller for handling ManuFacturer objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ManuFacturersController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// ManuFacturer controller constructor.
        /// </summary>
        /// <param name="bll">Interface with services via which repositories are accessed.</param>

        public ManuFacturersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ManuFacturers
        /// <summary>
        /// Get ManuFacturer objects associated with currently logged in user.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Array of ManuFacturer objects with count of products in each.</returns>
        /// <response code="200">The array of all ManuFacturers was successfully retrieved.</response>
        /// <response code="400">PageIndex or PageSize provided were smaller than 1.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.ManuFacturerWithProductCount>>> GetManuFacturers(string search, int? pageIndex, int? pageSize)
        {
            if ((pageIndex != null && pageIndex < 1) || (pageSize != null && pageSize < 1))
            {
                return BadRequest();
            }
            var manuFacturer = (await _bll.ManuFacturers.GetAllWithProductCountAsync(User.GetShopId(), search, pageIndex, pageSize)).Select(e => ManuFacturerMapper.MapFromBLL(e)).ToList();
            return manuFacturer;
        }
        
        /// <summary>
        /// Get ManuFacturer objects count associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <returns>Array of Categories with count of products in each.</returns>
        /// <response code="200">The array of all Categories was successfully retrieved.</response>
        [HttpGet("GetDataAmount")]
        public async Task<ActionResult<int>> GetDataAmount(string search)
        {
            var manuFacturer = await _bll.ManuFacturers.CountDataAmount(User.GetShopId(), search);
            return manuFacturer;
        }

        // GET: api/ManuFacturers/5
        /// <summary>
        /// Get ManuFacturer object associated with currently logged in user shop by provided identifier.        
        /// </summary>
        /// <param name="id">The identifier by which the ManuFacturer object is being searched for.</param>
        /// <returns>ManuFacturer object with count of products in it</returns>
        /// <response code="200">The ManuFacturer was successfully retrieved.</response>
        /// <response code="404">ManuFacturer object with provided identifier was not found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.ManuFacturerWithProductCount>> GetManuFacturer(int id)
        {
            var manuFacturer = await _bll.ManuFacturers.FindByIdAndShop(id, User.GetShopId());

            if (manuFacturer == null)
            {
                return NotFound();
            }

            return ManuFacturerMapper.MapFromBLL(manuFacturer);
        }

        // PUT: api/ManuFacturers/5
        /// <summary>
        /// Edit a ManuFacturer object.
        /// </summary>
        /// <param name="id">Identifier of the ManuFacturer object to be edited.</param>
        /// <param name="manuFacturer">ManuFacturer object containing new data.</param>
        /// <response code="204">The ManuFacturer was successfully edited.</response>
        /// <response code="400">New data provided for ManuFacturer is not valid or provided identifier does not match ManuFacturer object identifier.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManuFacturer(int id, PublicApi.v1.DTO.ManuFacturer manuFacturer)
        {
            if (!ModelState.IsValid || id != manuFacturer.Id)
            {
                return BadRequest();
            }

            _bll.ManuFacturers.Update(ManuFacturerMapper.MapFromExternal(manuFacturer));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/ManuFacturers
        /// <summary>
        /// Create new ManuFacturer object
        /// </summary>
        /// <param name="manuFacturer">ManuFacturer object to create</param>
        /// <response code="201">The ManuFacturer was successfully created.</response>
        /// <response code="400">The provided data via ManuFacturer object is not valid.</response>
        [HttpPost]
        public async Task<ActionResult<ManuFacturer>> PostManuFacturer(PublicApi.v1.DTO.ManuFacturer manuFacturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            manuFacturer = PublicApi.v1.Mappers.ManuFacturerMapper
                .MapFromBLL(await _bll.ManuFacturers.AddAsync(PublicApi.v1.Mappers.ManuFacturerMapper.MapFromExternal(manuFacturer)));

            await _bll.SaveChangesAsync();
            
            manuFacturer = PublicApi.v1.Mappers.ManuFacturerMapper.MapFromBLL(
                _bll.ManuFacturers.GetUpdatesAfterUOWSaveChanges(PublicApi.v1.Mappers.ManuFacturerMapper.MapFromExternal(manuFacturer)));

            return CreatedAtAction("GetManuFacturer", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = manuFacturer.Id }, manuFacturer);
        }

        // DELETE: api/ManuFacturers/5
        /// <summary>
        /// Delete ManuFacturer object
        /// </summary>
        /// <param name="id">Identifier of the ManuFacturer object to be deleted.</param>
        /// <response code="200">The ManuFacturer was successfully deleted.</response>
        /// <response code="400">The ManuFacturer object to be deleted has Product objects referenced to it.</response>
        /// <response code="404">There was no ManuFacturer object found with provided identifier.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteManuFacturer(int id)
        {
            var manuFacturerDto = await _bll.ManuFacturers.FindByIdAndShop(id, User.GetShopId());
            
            if (manuFacturerDto == null)
            {
                return NotFound();
            }

            if (manuFacturerDto.ProductCount != 0)
            {
                return NoContent();
            }

            _bll.ManuFacturers.Remove(id);
            await _bll.SaveChangesAsync();

            return Ok();
        }
    }
}
