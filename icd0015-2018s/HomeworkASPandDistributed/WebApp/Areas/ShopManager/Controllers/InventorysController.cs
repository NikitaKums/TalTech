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
    public class InventorysController : Controller
    {
        private readonly IAppBLL _bll;

        public InventorysController(IAppBLL bll)
        {
            _bll = bll;
        }



        // GET: Inventorys
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DescriptionSortParam"] = string.IsNullOrEmpty(sortOrder) ? "description_desc" : "";
            ViewData["CreatedAtSortParam"] = sortOrder == "createdAt" ? "createdAt_desc" : "createdAt";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var inventory = await _bll.Inventories.AllAsyncByShop(User.GetShopId(), sortOrder, searchString, pageNumber ?? 1, 10);
            return View(PaginatedList<BLL.App.DTO.DomainLikeDTO.Inventory>.Create(inventory, pageNumber ?? 1, 10, 
                await _bll.Inventories.CountDataAmount(User.GetShopId(), searchString)));
            
        }

        // GET: Inventorys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _bll.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventorys/Create
        public async Task<IActionResult> Create()
        {
            var vm = new InventoryCreateViewModel()
            {
                ShopSelectList = new SelectList(await _bll.Shops.GetShopByUserShopIdForDropDown(User.GetShopId()),
                    nameof(Shop.Id), nameof(Shop.ShopName))

            };
            return View(vm);
        }

        // POST: Inventorys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventoryCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _bll.Inventories.AddAsync(vm.Inventory);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.ShopSelectList = new SelectList(await _bll.Shops.GetShopByUserShopIdForDropDown(User.GetShopId()), nameof(Shop.Id), nameof(Shop.ShopName));
            return View(vm);
        }

        // GET: Inventorys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _bll.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            var vm = new InventoryCreateViewModel()
            {
                Inventory = inventory,
                ShopSelectList = new SelectList(await _bll.Shops.GetShopByUserShopIdForDropDown(User.GetShopId()), nameof(Shop.Id), nameof(Shop.ShopName))
            };
            
            return View(vm);
        }

        // POST: Inventorys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InventoryCreateViewModel vm)
        {
            if (id != vm.Inventory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Inventories.Update(vm.Inventory);
                await _bll.SaveChangesAsync();
               
                return RedirectToAction(nameof(Index));
            }
            vm.ShopSelectList = new SelectList(await _bll.Shops.GetShopByUserShopIdForDropDown(User.GetShopId()), nameof(Shop.Id), nameof(Shop.ShopName));
            return View(vm);
        }

        // GET: Inventorys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _bll.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventorys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _bll.Inventories.Remove(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
