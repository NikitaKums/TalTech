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
    /// Controller for handling Order objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// Orders controller constructor.
        /// </summary>
        /// <param name="bll">Interface with services via which repositories are accessed.</param>
        public OrdersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Orders
        /// <summary>
        /// Get Order objects associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Array of Orders with count of products in each.</returns>
        /// <response code="200">The array of all Orders was successfully retrieved.</response>
        /// <response code="400">PageIndex or PageSize provided were smaller than 1.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.OrderWithProductCount>>> GetOrders(string search, int? pageIndex, int? pageSize)
        {
            if ((pageIndex != null && pageIndex < 1) || (pageSize != null && pageSize < 1))
            {
                return BadRequest();
            }
            var order = (await _bll.Orders.GetAllByShopDTOAsync(User.GetShopId(), search, pageIndex, pageSize)).Select(e => OrderMapper.MapFromBLL(e)).ToList();
            return order;
        }
        
        /// <summary>
        /// Get Order objects count associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <returns>Array of Categories with count of products in each.</returns>
        /// <response code="200">The array of all Categories was successfully retrieved.</response>
        [HttpGet("GetDataAmount")]
        public async Task<ActionResult<int>> GetDataAmount(string search)
        {
            var order = await _bll.Orders.CountDataAmount(User.GetShopId(), search);
            return order;
        }

        // GET: api/Orders/5
        /// <summary>
        /// Get Order object associated with currently logged in user shop by provided identifier.        
        /// </summary>
        /// <param name="id">The identifier by which the Order object is being searched for.</param>
        /// <returns>Order object with count of products in it</returns>
        /// <response code="200">The Order was successfully retrieved.</response>
        /// <response code="404">Order object with provided identifier was not found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.OrderWithProductCount>> GetOrder(int id)
        {
            var order = await _bll.Orders.FindByShopAndIdAsync(id, User.GetShopId());

            if (order == null)
            {
                return NotFound();
            }

            return OrderMapper.MapFromBLL(order);
        }
        
        // GET: api/Orders/5
        /// <summary>
        /// Get OrderReceived objects associated with currently logged in user shop by provided identifier.        
        /// </summary>
        /// <param name="id">The identifier by which the OrderReceived object is being searched for.</param>
        /// <returns>OrderReceived objects</returns>
        /// <response code="200">The array of OrderReceived objects was successfully retrieved.</response>
        /// <response code="404">No OrderReceived objects were found with provided identifier.</response>
        [HttpGet("GetOrderReceived/{id}")]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.OrderReceived>>> GetOrderReceived(int id)
        {
            var order = await _bll.ProductsInOrder.FindOrdersReceivedByOrderId(id, User.GetShopId());

            if (order == null)
            {
                return NotFound();
            }

            return order.Select(e => OrderMapper.MapFromBLL(e)).ToList();
        }

        // PUT: api/Orders/5
        /// <summary>
        /// Edit a Order object.
        /// </summary>
        /// <param name="id">Identifier of the Order object to be edited.</param>
        /// <param name="order">Order object containing new data.</param>
        /// <response code="204">The Order was successfully edited.</response>
        /// <response code="400">New data provided for Order is not valid or provided identifier does not match Order object identifier or order shop id doesn't match user shop id.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, PublicApi.v1.DTO.Order order)
        {
            if (!ModelState.IsValid || id != order.Id || order.ShopId != User.GetShopId())
            {
                return BadRequest();
            }

            _bll.Orders.Update(OrderMapper.MapFromExternal(order));
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Orders
        /// <summary>
        /// Create new Order object
        /// </summary>
        /// <param name="order">Order object to create</param>
        /// <response code="201">The Order was successfully created.</response>
        /// <response code="400">The provided data via Order object is not valid or order shop id doesn't match user shop id.</response>
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(PublicApi.v1.DTO.Order order)
        {
            if (!ModelState.IsValid || order.ShopId != User.GetShopId())
            {
                return BadRequest();
            }
            
            order = PublicApi.v1.Mappers.OrderMapper
                .MapFromBLL(await _bll.Orders.AddAsync(PublicApi.v1.Mappers.OrderMapper.MapFromExternal(order)));

            await _bll.SaveChangesAsync();
            
            order = PublicApi.v1.Mappers.OrderMapper.MapFromBLL(
                _bll.Orders.GetUpdatesAfterUOWSaveChanges(PublicApi.v1.Mappers.OrderMapper.MapFromExternal(order)));

            return CreatedAtAction("GetOrder", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        /// <summary>
        /// Delete Order object
        /// </summary>
        /// <param name="id">Identifier of the Order object to be deleted.</param>
        /// <response code="200">The Order was successfully deleted.</response>
        /// <response code="400">The Order object to be deleted has Product objects referenced to it.</response>
        /// <response code="404">There was no Order object found with provided identifier or order shop id doesn't match user shop id.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var order = await _bll.Orders.FindAsync(id);
            if (order == null || order.ShopId != User.GetShopId())
            {
                return NotFound();
            }

            if (order.ProductsInOrder.Count != 0)
            {
                return NoContent();
            }

            _bll.Orders.Remove(id);
            await _bll.SaveChangesAsync();

            return Ok();
        }
        
        
        // POST: api/Orders
        /// <summary>
        /// Process received order -> increase product quantity according to those in order.
        /// </summary>
        /// <param name="id">Id of the received order</param>
        /// <response code="200">The Order was successfully processes.</response>
        [HttpPost("AllOrdersReceived/{id}")]
        public async Task<ActionResult<Order>> AllOrdersReceived(int id)
        {
            await _bll.Orders.ProcessReceivedOrder(id);
            return Ok();
        }

    }
}
