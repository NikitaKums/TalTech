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
    /// Controller for handling Sale objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SalesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Sales controller constructor.
        /// </summary>
        /// <param name="bll">Interface with services via which repositories are accessed.</param>
        public SalesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/Sales
        /// <summary>
        /// Get Sale objects associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Array of Sale with count of products in each.</returns>
        /// <response code="200">The array of all Sale was successfully retrieved.</response>
        /// <response code="400">PageIndex or PageSize provided were smaller than 1.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.Sale>>> GetSales(string search, int? pageIndex, int? pageSize)
        {
            if ((pageIndex != null && pageIndex < 1) || (pageSize != null && pageSize < 1))
            {
                return BadRequest();
            }
            var sale = (await _bll.Sales.AllAsyncByShopDTO(User.GetShopId(), search, pageIndex, pageSize)).Select(e => SaleMapper.MapFromBLL(e)).ToList();
            return sale;
        }
        
        /// <summary>
        /// Get Sale objects count associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <returns>Array of Categories with count of products in each.</returns>
        /// <response code="200">The array of all Categories was successfully retrieved.</response>
        [HttpGet("GetDataAmount")]
        public async Task<ActionResult<int>> GetDataAmount(string search)
        {
            var sale = await _bll.Sales.CountDataAmount(User.GetShopId(), search);
            return sale;
        }
        
        /// <summary>
        /// Get total price of sold items.
        /// </summary>
        /// <returns>Dictionary of total prices.</returns>
        /// <response code="200">The dictionary of total prices. was successfully retrieved.</response>
        [HttpGet("GetTotalSaleAmounts")]
        public async Task<Dictionary<string, decimal?>> GetTotalSaleAmounts()
        {
            var sale = await _bll.Sales.GetSaleAmounts(User.GetShopId());
            return sale;
        }
        
        /// <summary>
        /// Get SaleIdName objects associated with currently logged in user shop.        
        /// </summary>
        /// <returns>Array of SaleIdName objects with count of products in it</returns>
        /// <response code="200">Array of SaleIdName objects was successfully retrieved.</response>
        [HttpGet]
        [Route("GetSalesForUser")]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.IdAndNameDTO.SaleIdName>>> GetSalesForUser()
        {
            var sale = (await _bll.Sales.GetAsyncByShopAndUserIdDTO(User.GetUserId(), User.GetShopId())).Select(e => SaleMapper.MapFromBLLIdName(e)).ToList();
            return sale;
        }

        // GET: api/Sales/5
        /// <summary>
        /// Get Sale object associated with currently logged in user shop by provided identifier.        
        /// </summary>
        /// <param name="id">The identifier by which the Sale object is being searched for.</param>
        /// <returns>Sale object with count of products in it</returns>
        /// <response code="200">The Sale was successfully retrieved.</response>
        /// <response code="404">Sale object with provided identifier was not found.</response>			
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.Sale>> GetSale(int id)
        {
            var sale = await _bll.Sales.GetAsyncByShopAndIdDTO(id, User.GetShopId());

            if (sale == null)
            {
                return NotFound();
            }

            return SaleMapper.MapFromBLL(sale);
        }

        // PUT: api/Sales/5
        /// <summary>
        /// Edit a Sale object.
        /// </summary>
        /// <param name="id">Identifier of the Sale object to be edited.</param>
        /// <param name="sale">Sale object containing new data.</param>
        /// <response code="204">The Sale was successfully edited.</response>
        /// <response code="400">New data provided for Sale is not valid or provided identifier does not match Sale object identifier or sale user id doesn't match user id.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(int id, PublicApi.v1.DTO.Sale sale)
        {
            if (!ModelState.IsValid || id != sale.Id || sale.AppUserId != User.GetUserId())
            {
                return BadRequest();
            }

            _bll.Sales.Update(SaleMapper.MapFromExternal(sale));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Sales
        /// <summary>
        /// Create new Sale object
        /// </summary>
        /// <param name="sale">Sale object to create</param>
        /// <response code="201">The Sale was successfully created.</response>
        /// <response code="400">The provided data via Sale object is not valid or sale user id doesn't match user id.</response>
        [HttpPost]
        public async Task<ActionResult<PublicApi.v1.DTO.Sale>> PostSale(PublicApi.v1.DTO.Sale sale)
        {
            if (!ModelState.IsValid || sale.AppUserId != User.GetUserId())
            {
                return BadRequest();
            }
            
            sale = PublicApi.v1.Mappers.SaleMapper
                .MapFromBLL(await _bll.Sales.AddAsync(PublicApi.v1.Mappers.SaleMapper.MapFromExternal(sale)));

            await _bll.SaveChangesAsync();
            
            sale = PublicApi.v1.Mappers.SaleMapper.MapFromBLL(
                _bll.Sales.GetUpdatesAfterUOWSaveChanges(PublicApi.v1.Mappers.SaleMapper.MapFromExternal(sale)));

            return CreatedAtAction("GetSale", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = sale.Id }, sale);
        }

        // DELETE: api/Sales/5
        /// <summary>
        /// Delete Sale object
        /// </summary>
        /// <param name="id">Identifier of the Sale object to be deleted.</param>
        /// <response code="200">The Sale was successfully deleted.</response>
        /// <response code="400">The Sale object to be deleted has Product objects referenced to it.</response>
        /// <response code="404">There was no Sale object found with provided identifier or sale user id doesn't match user id.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSale(int id)
        {
            var sale = await _bll.Sales.FindAsync(id);
            if (sale == null || sale.AppUserId != User.GetUserId())
            {
                return NotFound();
            }

            if (await _bll.ProductsSold.CountProductsInSale(id) != 0)
            {
                return BadRequest();
            }
            
            _bll.Sales.Remove(id);
            await _bll.SaveChangesAsync();

            return Ok();
        }

    }
}
