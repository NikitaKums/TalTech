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
using ee.itcollege.nikita.Identity;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Areas.ShopManager.Controllers
{
    [Authorize(Roles = "ShopManager, Admin")]
    [Area("ShopManager")]
    public class CategoriesController : Controller
    {
        private readonly IAppBLL _bll;

        public CategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Categories
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var category =  await _bll.Categories.AllAsyncByShop(User.GetShopId(), sortOrder, searchString, pageNumber ?? 1, 10);
            
            return View(PaginatedList<BLL.App.DTO.DomainLikeDTO.Category>.Create(category, pageNumber ?? 1, 10, 
                await _bll.Categories.CountDataAmount(User.GetShopId(), searchString)));
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _bll.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public async Task<IActionResult> Create()
        {
            var vm = new CategoryCreateViewModel()
            {
                ShopSelectList = new SelectList(await _bll.Shops.GetShopByUserShopIdForDropDown(User.GetShopId()), nameof(Shop.Id), nameof(Shop.ShopName))
            };
            
            return View(vm);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _bll.Categories.AddAsync(vm.Category);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.ShopSelectList = new SelectList(await _bll.Shops.GetShopByUserShopIdForDropDown(User.GetShopId()), nameof(Shop.Id), nameof(Shop.ShopName));
            return View(vm);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _bll.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var vm = new CategoryCreateViewModel()
            {
                Category = category,
                ShopSelectList = new SelectList(await _bll.Shops.GetShopByUserShopIdForDropDown(User.GetShopId()), nameof(Shop.Id), nameof(Shop.ShopName), category.ShopId)
            };
            return View(vm);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryCreateViewModel vm)
        {
            if (id != vm.Category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Categories.Update(vm.Category);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.ShopSelectList =  new SelectList(await _bll.Shops.GetShopByUserShopIdForDropDown(User.GetShopId()), nameof(Shop.Id), nameof(Shop.ShopName), vm.Category.ShopId);
            return View(vm);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _bll.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bll.Categories.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }

        public RedirectToActionResult AddProductToCategory(int id)
        {
            return RedirectToAction(nameof(Create), "ProductsInCategory", new {id = id});
        }
    }
}
