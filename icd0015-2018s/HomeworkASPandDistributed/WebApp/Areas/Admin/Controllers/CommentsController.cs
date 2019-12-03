using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CommentsController : Controller
    {
        private readonly IAppBLL _bll;

        public CommentsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: Comments
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParam"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["BodySortParam"] = sortOrder == "body" ? "body_desc" : "body";
            ViewData["ProductSortParam"] = sortOrder == "product" ? "product_desc" : "product";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            
            var comments = await _bll.Comments.AllAsync(sortOrder, searchString, pageNumber ?? 1, 10);
            
            return View(PaginatedList<BLL.App.DTO.DomainLikeDTO.Comment>.Create(comments, pageNumber ?? 1, 10, 
                await _bll.Comments.CountDataAmount(searchString)));
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var comment = await _context.Comments
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);*/

            var comment = await _bll.Comments.FindAsync(id);
            
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }


        // GET: Comments/Create
        public async Task<IActionResult> Create()
        {
            var vm = new CommentCreateViewModel
            {
                ProductSelectList = new SelectList(await _bll.Products.AllAsync(),
                    nameof(Product.Id), nameof(Product.ProductName)),
                ShopSelectList = new SelectList(await _bll.Shops.AllAsync(),
                    nameof(Shop.Id), nameof(Shop.ShopName))
            };

            return View(vm);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommentCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _bll.Comments.AddAsync(vm.Comment);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.ProductSelectList= new SelectList(await _bll.Products.AllAsync(),
                nameof(Product.Id), nameof(Product.ProductName), vm.Comment.ProductId);
            vm.ShopSelectList = new SelectList(await _bll.Shops.AllAsync(),
                nameof(Shop.Id), nameof(Shop.ShopName), vm.Comment.ShopId);
            return View(vm);
        }


        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _bll.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }
            
            var vm = new CommentCreateViewModel
            {
                Comment = comment,
                ProductSelectList = new SelectList(await _bll.Products.AllAsync(),
                    nameof(Product.Id), nameof(Product.ProductName), comment.ProductId),
                ShopSelectList = new SelectList(await _bll.Shops.AllAsync(),
                    nameof(Shop.Id), nameof(Shop.ShopName), comment.ShopId)
            };

            return View(vm);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CommentCreateViewModel vm)
        {
            if (id != vm.Comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Comments.Update(vm.Comment);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            vm.ProductSelectList= new SelectList(await _bll.Products.AllAsync(),
                nameof(Product.Id), nameof(Product.ProductName), vm.Comment.ProductId);
            vm.ShopSelectList = new SelectList(await _bll.Shops.AllAsync(),
                nameof(Shop.Id), nameof(Shop.ShopName), vm.Comment.ShopId);
            return View(vm);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _bll.Comments.FindAsync(id);

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
            _bll.Comments.Remove(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
