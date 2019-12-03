using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain;
using ee.itcollege.nikita.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly IAppUnitOfWork  _unitOfWork;

        public CommentsController(IAppUnitOfWork  unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Comments
        public async Task<IActionResult> Index(string search)
        {
            ViewBag.search = search;
            var res = await _unitOfWork.Comments.AllAsyncWithSearch(User.GetUserId(), search);
            return View(res);
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _unitOfWork.Comments.FindAsyncById(id.Value, User.GetUserId());

            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            //ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentTitle,CommentBody,AppUserId,Id")]
            Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.AppUserId = User.GetUserId();
                await _unitOfWork.Comments.AddAsync(comment);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", comment.AppUserId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _unitOfWork.Comments.FindAsyncById(id.Value, User.GetUserId());
            if (comment == null)
            {
                return NotFound();
            }

            //ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", comment.AppUserId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentTitle,CommentBody,AppUserId,Id")]
            Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                comment.AppUserId = User.GetUserId();
                _unitOfWork.Comments.Update(comment);
                await _unitOfWork.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }

            //ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", comment.AppUserId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _unitOfWork.Comments.FindAsyncById(id.Value, User.GetUserId());
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _unitOfWork.Comments.FindAsync(id);
            if (comment.AppUserId != User.GetUserId())
            {
                return RedirectToAction(nameof(Index));
            }

            _unitOfWork.Comments.Remove(comment);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}