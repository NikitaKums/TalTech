using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using ee.itcollege.nikita.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CommentsController : ControllerBase
    {
        private readonly IAppUnitOfWork  _unitOfWork;

        public CommentsController(IAppUnitOfWork  unitOfWork)
        {
            _unitOfWork = unitOfWork;
        } 

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DAL.App.DTO.Comment>>> GetComments(string search)
        {
            var res = await _unitOfWork.Comments.AllAsyncWithSearchAPI(User.GetUserId(), search);

            return res;
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DAL.App.DTO.Comment>> GetComment(int id)
        {
            var comment = await _unitOfWork.Comments.FindAsyncByIdAPI(id, User.GetUserId());

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            comment.AppUserId = User.GetUserId();
            if (id != comment.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Comments.Update(comment);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Comments
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            comment.AppUserId = User.GetUserId();
            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);

        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> DeleteComment(int id)
        {
            var comment = await _unitOfWork.Comments.FindAsync(id);
            if (comment == null || comment.AppUserId != User.GetUserId())
            {
                return NotFound();
            }

            _unitOfWork.Comments.Remove(comment);
            await _unitOfWork.SaveChangesAsync();

            return comment;

        }

        
    }
}
