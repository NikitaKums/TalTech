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
    /// Controller for handling Defect objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DefectsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Defects controller constructor.
        /// </summary>
        /// <param name="bll">Interface with services via which repositories are accessed.</param>

        public DefectsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Defects
        /// <summary>
        /// Get Defect objects associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Array of Defect objects with count of products in each.</returns>
        /// <response code="200">The array of all Defects was successfully retrieved.</response>
        /// <response code="400">PageIndex or PageSize provided were smaller than 1.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.Defect>>> GetDefects(string search, int? pageIndex, int? pageSize)
        {
            if ((pageIndex != null && pageIndex < 1) || (pageSize != null && pageSize < 1))
            {
                return BadRequest();
            }
            var defect = (await _bll.Defects.GetAllWithProductsWithDefectByShopAsync(User.GetShopId(), search, pageIndex, pageSize)).Select(e => DefectMapper.MapFromBLL(e)).ToList();
            return defect;
        }
        
        /// <summary>
        /// Get Defect objects count associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <returns>Array of Categories with count of products in each.</returns>
        /// <response code="200">The array of all Categories was successfully retrieved.</response>
        [HttpGet("GetDataAmount")]
        public async Task<ActionResult<int>> GetDataAmount(string search)
        {
            var defect = await _bll.Defects.CountDataAmount(User.GetShopId(), search);
            return defect;
        }

        
        // GET: api/Defects/5
        /// <summary>
        /// Get Defect object associated with currently logged in user shop by provided identifier.
        /// </summary>
        /// <param name="id">The identifier by which the Defect object is being searched for.</param>
        /// <returns>Defect object with count of products in it</returns>
        /// <response code="200">The Defect was successfully retrieved.</response>
        /// <response code="404">Defect object with provided identifier was not found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.Defect>> GetDefect(int id)
        {
            var defect = await _bll.Defects.FindProductsWithDefectByShopAsync(id, User.GetShopId());

            if (defect == null)
            {
                return NotFound();
            }

            return DefectMapper.MapFromBLL(defect);
        }

        // PUT: api/Defects/5
        /// <summary>
        /// Edit a Defect object.
        /// </summary>
        /// <param name="id">Identifier of the Defect object to be edited.</param>
        /// <param name="defect">Defect object containing new data.</param>
        /// <response code="204">The Defect was successfully edited.</response>
        /// <response code="400">New data provided for Defect is not valid or provided identifier does not match Defect object identifier or provided Defect shop identifier does not match logged in user shop identifier.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDefect(int id, PublicApi.v1.DTO.Defect defect)
        {
            if (!ModelState.IsValid || id != defect.Id || defect.ShopId != User.GetShopId())
            {
                return BadRequest();
            }

            _bll.Defects.Update(DefectMapper.MapFromExternal(defect));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Defects
        /// <summary>
        /// Create new Defect object
        /// </summary>
        /// <param name="defect">Defect object to create</param>
        /// <response code="201">The Defect was successfully created.</response>
        /// <response code="400">The provided data via Defect object is not valid.</response>
        [HttpPost]
        public async Task<ActionResult<Defect>> PostDefect(PublicApi.v1.DTO.Defect defect)
        {
            if (!ModelState.IsValid || defect.ShopId != User.GetShopId())
            {
                return BadRequest();
            }
            
            defect = PublicApi.v1.Mappers.DefectMapper
                .MapFromBLL(await _bll.Defects.AddAsync(PublicApi.v1.Mappers.DefectMapper.MapFromExternal(defect)));

            await _bll.SaveChangesAsync();
            
            defect = PublicApi.v1.Mappers.DefectMapper.MapFromBLL(
                _bll.Defects.GetUpdatesAfterUOWSaveChanges(PublicApi.v1.Mappers.DefectMapper.MapFromExternal(defect)));

            return CreatedAtAction("GetDefect", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = defect.Id }, defect);
        }

        // DELETE: api/Defects/5
        /// <summary>
        /// Delete Defect object
        /// </summary>
        /// <param name="id">Identifier of the Defect object to be deleted.</param>
        /// <response code="200">The Defect was successfully deleted.</response>
        /// <response code="400">The Defect object to be deleted has Product objects referenced to it.</response>
        /// <response code="404">There was no Defect object found with provided identifier.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDefect(int id)
        {
            var defect = await _bll.Defects.FindAsync(id);
            if (defect == null)
            {
                return NotFound();
            }

            if (await _bll.ProductsWithDefect.CountDefectItems(id) != 0)
            {
                return BadRequest();
            }
            
            if (defect.ShopId != User.GetShopId())
            {
                return BadRequest();
            }

            _bll.Defects.Remove(id);
            await _bll.SaveChangesAsync();

            return Ok();
        }

    }
}
