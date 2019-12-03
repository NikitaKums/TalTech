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
    /// Controller for handling Comment objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CommentsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Comments controller constructor.
        /// </summary>
        /// <param name="bll">Interface with services via which repositories are accessed.</param>
        public CommentsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Comments
        /// <summary>
        /// Get all Comment objects associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>Array of Comment objects.</returns>
        /// <response code="200">The array of Comment objects was successfully retrieved.</response>
        /// <response code="400">PageIndex or PageSize provided were smaller than 1.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.Comment>>> GetComments(string search, int? pageIndex, int? pageSize)
        {
            if ((pageIndex != null && pageIndex < 1) || (pageSize != null && pageSize < 1))
            {
                return BadRequest();
            }
            //var comment = await _bll.Comments.AllAsync();
            var comment = (await _bll.Comments.GetAllWithProductByShopAsync(User.GetShopId(), search, pageIndex, pageSize)).Select(e => CommentMapper.MapFromBLL(e)).ToList();

            return comment;
        }
        
        /// <summary>
        /// Get Comment objects count associated with currently logged in user shop.
        /// </summary>
        /// <param name="search">Search query string.</param>
        /// <returns>Array of Categories with count of products in each.</returns>
        /// <response code="200">The array of all Categories was successfully retrieved.</response>
        [HttpGet("GetDataAmount")]
        public async Task<ActionResult<int>> GetDataAmount(string search)
        {
            var comment = await _bll.Comments.CountDataAmount(User.GetShopId(), search);
            return comment;
        }

        /// <summary>
        /// Get Comment object associated with currently logged in user by provided identifier and by user's shop identifier.
        /// </summary>
        /// <param name="id">The identifier by which the Comment object is being searched for.</param>
        /// <returns>Comment object</returns>
        /// <response code="404">No Comment object was found that would match given identifiers.</response>
        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.Comment>> GetComment(int id)
        {
            var comment = await _bll.Comments.GetCommentByIdAndShop(id, User.GetShopId());

            if (comment == null)
            {
                return NotFound();
            }

            return CommentMapper.MapFromBLL(comment);
        }

        /// <summary>
        /// Edit a Comment object .
        /// </summary>
        /// <param name="id">Identifier of the Comment object to be edited.</param>
        /// <param name="comment">Comment object containing new data.</param>
        /// <response code="204">The Comment was successfully edited.</response>
        /// <response code="400">New data provided for Comment is not valid or provided identifier does not match Comment object identifier or provided Comment shop identifier does not match logged in user shop identifier.</response>
        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, PublicApi.v1.DTO.Comment comment)
        {
            if (!ModelState.IsValid || id != comment.Id || comment.ShopId != User.GetShopId())
            {
                return BadRequest();
            }

            _bll.Comments.Update(CommentMapper.MapFromExternal(comment));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new Comment object
        /// </summary>
        /// <param name="comment">Comment object to create</param>
        /// <response code="201">The Comment was successfully created.</response>
        /// <response code="400">The provided data via Comment object is not valid or provided Comment shop identifier does not match logged in user shop identifier.</response>
        // POST: api/Comments
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(PublicApi.v1.DTO.Comment comment)
        {
            if (!ModelState.IsValid || comment.ShopId != User.GetShopId())
            {
                return BadRequest();
            }
            
            comment = PublicApi.v1.Mappers.CommentMapper
                .MapFromBLL(await _bll.Comments.AddAsync(PublicApi.v1.Mappers.CommentMapper.MapFromExternal(comment)));

            await _bll.SaveChangesAsync();
            
            comment = PublicApi.v1.Mappers.CommentMapper.MapFromBLL(
                _bll.Comments.GetUpdatesAfterUOWSaveChanges(PublicApi.v1.Mappers.CommentMapper.MapFromExternal(comment)));

            return CreatedAtAction("GetComment", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = comment.Id }, comment);
        }

        /// <summary>
        /// Delete Comment object
        /// </summary>
        /// <param name="id">Identifier of the Comment object to be deleted.</param>
        /// <response code="200">The Comment was successfully deleted.</response>
        /// <response code="400">Provided Comment shop identifier does not match logged in user shop identifier.</response>
        /// <response code="404">There was no Comment object found with provided identifier.</response>
        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            var comment = await _bll.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            
            if (comment.ShopId != User.GetShopId())
            {
                return BadRequest();
            }

            _bll.Comments.Remove(id);
            await _bll.SaveChangesAsync();

            return Ok();

        }

    }
}
