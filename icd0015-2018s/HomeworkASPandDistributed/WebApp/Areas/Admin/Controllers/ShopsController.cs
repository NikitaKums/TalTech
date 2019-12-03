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
    public class ShopsController : Controller
    {
        
        private readonly IAppBLL _bll;

        public ShopsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Shops
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AddressSortParam"] = sortOrder == "address" ? "address_desc" : "address";
            ViewData["ContactSortParam"] = sortOrder == "contact" ? "contact_desc" : "contact";
            ViewData["Contact2SortParam"] = sortOrder == "contact2" ? "contact2_desc" : "contact2";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var shop = await _bll.Shops.AllAsync(sortOrder, searchString, pageNumber ?? 1, 10);
            return View(PaginatedList<BLL.App.DTO.DomainLikeDTO.Shop>.Create(shop, pageNumber ?? 1, 10, 
                await _bll.Shops.CountDataAmount(searchString)));
        }

        // GET: Shops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shop = await _bll.Shops.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }

            return View(shop);
        }

        // GET: Shops/Create
        public IActionResult Create()
        {
            return View(new ShopCreateViewModel());
        }

        // POST: Shops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShopCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _bll.Shops.AddAsync(vm.Shop);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Shops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shop = await _bll.Shops.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }
            var vm = new ShopCreateViewModel()
            {
                Shop = shop
            };
            return View(vm);
        }

        // POST: Shops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ShopCreateViewModel vm)
        {
            if (id != vm.Shop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Shops.Update(vm.Shop);
                await _bll.SaveChangesAsync();
               
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Shops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shop = await _bll.Shops.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }

            return View(shop);
        }

        // POST: Shops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _bll.Shops.Remove(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
