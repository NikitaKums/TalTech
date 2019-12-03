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
using Domain.Identity;
using ee.itcollege.nikita.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")] 
    [Area("Admin")]
    public class DefectsController : Controller
    {
        private readonly IAppBLL _bll;

        public DefectsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Defects
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
            
            var defect = await _bll.Defects.AllAsync(sortOrder, searchString, pageNumber ?? 1, 10);
            
            return View(PaginatedList<BLL.App.DTO.DomainLikeDTO.Defect>.Create(defect, pageNumber ?? 1, 10, 
                await _bll.Defects.CountDataAmount(searchString)));

        }

        // GET: Defects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var defect = await _context.Defects
                .FirstOrDefaultAsync(m => m.Id == id);*/

            var defect = await _bll.Defects.FindAsync(id);
            if (defect == null)
            {
                return NotFound();
            }

            return View(defect);

        }

        // GET: Defects/Create
        public async Task<IActionResult> Create()
        {
            var vm = new DefectCreateViewModel()
            {
                ShopSelectList = new SelectList(await _bll.Shops.AllAsync(),
                    nameof(Shop.Id), nameof(Shop.ShopName))

            };
            return View(vm);
        }

        // POST: Defects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DefectCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _bll.Defects.AddAsync(vm.Defect);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.ShopSelectList = new SelectList(await _bll.Shops.AllAsync(), nameof(Shop.Id), nameof(Shop.ShopName), nameof(vm.Defect.ShopId));
            return View(vm);
        }

        // GET: Defects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defect = await _bll.Defects.FindAsync(id);
            if (defect == null)
            {
                return NotFound();
            }
            
            var vm = new DefectCreateViewModel()
            {
                Defect = defect,
                ShopSelectList = new SelectList(await _bll.Shops.AllAsync(), nameof(Shop.Id), nameof(Shop.ShopName), nameof(defect.ShopId))

            };
            
            return View(vm);
        }

        // POST: Defects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DefectCreateViewModel vm)
        {
            if (id != vm.Defect.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               _bll.Defects.Update(vm.Defect);
               await _bll.SaveChangesAsync();
                
               return RedirectToAction(nameof(Index));
            }
            
            vm.ShopSelectList = new SelectList(await _bll.Shops.AllAsync(), nameof(Shop.Id), nameof(Shop.ShopName), nameof(vm.Defect.ShopId));
            return View(vm);
        }

        // GET: Defects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defect = await _bll.Defects.FindAsync(id);
            if (defect == null)
            {
                return NotFound();
            }

            return View(defect);
        }

        // POST: Defects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bll.Defects.DeleteDefect(id);
            return RedirectToAction(nameof(Index));
        }
        
        public RedirectToActionResult AddDefectToProduct(int id)
        {
            return RedirectToAction(nameof(Create), "ProductsWithDefect", new {id = id});
        }
    }
}
