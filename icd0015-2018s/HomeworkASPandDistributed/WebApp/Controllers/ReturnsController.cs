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

namespace WebApp.Controllers
{
    [Authorize]
    public class ReturnsController : Controller
    {
        private readonly IAppBLL _bll;

        public ReturnsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Returns
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DescriptionSortParam"] = string.IsNullOrEmpty(sortOrder) ? "description_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            
            var @return = await _bll.Returns.AllAsyncByShop(User.GetShopId(), sortOrder, searchString, pageNumber ?? 1, 10);
            return View(PaginatedList<BLL.App.DTO.DomainLikeDTO.Return>.Create(@return, pageNumber ?? 1, 10, 
                await _bll.Returns.CountDataAmount(User.GetShopId(), searchString)));
        }

        // GET: Returns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @return = await _bll.Returns.FindAsync(id);
            if (@return == null)
            {
                return NotFound();
            }

            return View(@return);
        }


        // GET: Returns/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ReturnCreateViewModel()
            {
                ShopSelectList = new SelectList(await _bll.Shops.GetShopByUserShopIdForDropDown(User.GetShopId()), nameof(Shop.Id), nameof(Shop.ShopName))
            };

            return View(vm);
        }

        // POST: Returns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReturnCreateViewModel vm)
        {
            if (ModelState.IsValid && vm.Return.ShopId == User.GetShopId())
            {
                await _bll.Returns.AddAsync(vm.Return);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.ShopSelectList = new SelectList(await _bll.Shops.GetShopByUserShopIdForDropDown(User.GetShopId()), nameof(Shop.Id), nameof(Shop.ShopName));
            return View(vm);
        }

        // GET: Returns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @return = await _bll.Returns.FindAsync(id);

            if (@return == null)
            {
                return NotFound();
            }
            
            var vm = new ReturnCreateViewModel()
            {
                Return = @return,
                ShopSelectList = new SelectList(await _bll.Shops.GetShopByUserShopIdForDropDown(User.GetShopId()), nameof(Shop.Id), nameof(Shop.ShopName))
            };
            
            return View(vm);
        }

        // POST: Returns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ReturnCreateViewModel vm)
        {
            if (id != vm.Return.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && vm.Return.ShopId == User.GetShopId())
            {
                _bll.Returns.Update(vm.Return);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.ShopSelectList = new SelectList(await _bll.Shops.GetShopByUserShopIdForDropDown(User.GetShopId()), nameof(Shop.Id), nameof(Shop.ShopName));
            return View(vm);
        }

        // GET: Returns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @return = await _bll.Returns.FindAsync(id);

            if (@return == null)
            {
                return NotFound();
            }

            return View(@return);
        }

        // POST: Returns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @return = await _bll.Returns.FindAsync(id);
            if (@return?.ShopId == User.GetShopId())
            {
                await _bll.Returns.DeleteReturn(id);
            }
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<RedirectToActionResult> AddProductToReturn(int id)
        {
            var @return = await _bll.Returns.FindAsync(id);
            if (@return?.ShopId != User.GetShopId())
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Create), "ProductsReturned", new {id = id});
        }
    }

}
