using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using Contracts.BLL.App;
using Contracts.DAL.App;
using DAL.App.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class ManuFacturersController : Controller
    {
        private readonly IAppBLL _bll;

        public ManuFacturersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: ManuFacturers
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AddressSortParam"] = sortOrder == "address" ? "address_desc" : "address";
            ViewData["NumberSortParam"] = sortOrder == "number" ? "number_desc" : "number";
            
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            
            var manuFacturer = await _bll.ManuFacturers.AllAsync(sortOrder, searchString, pageNumber ?? 1, 10);

            return View(PaginatedList<BLL.App.DTO.DomainLikeDTO.ManuFacturer>.Create(manuFacturer, pageNumber ?? 1, 10, 
                await _bll.ManuFacturers.CountDataAmount(User.GetShopId(), searchString)));
        }

        // GET: ManuFacturers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manuFacturer = await _bll.ManuFacturers.FindAsync(id);
                
            if (manuFacturer == null)
            {
                return NotFound();
            }

            return View(manuFacturer);
        }

        // GET: ManuFacturers/Create
        public IActionResult Create()
        {
            return View(new ManuFacturerCreateViewModel());
        }

        // POST: ManuFacturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManuFacturerCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _bll.ManuFacturers.AddAsync(vm.ManuFacturer);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View(vm);
        }

        // GET: ManuFacturers/Edit/5
        /*public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manuFacturer = await _bll.ManuFacturers.FindAsync(id);
            if (manuFacturer == null)
            {
                return NotFound();
            }
            return View(manuFacturer);
        }*/

        // POST: ManuFacturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ManuFacturerName,Aadress,PhoneNumber,Id")] ManuFacturer manuFacturer)
        {
            if (id != manuFacturer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.ManuFacturers.Update(manuFacturer);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View(manuFacturer);
        }*/

        // GET: ManuFacturers/Delete/5
       /* public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manuFacturer = await _bll.ManuFacturers.FindAsync(id);
            if (manuFacturer == null)
            {
                return NotFound();
            }

            return View(manuFacturer);
        }

        // POST: ManuFacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _bll.ManuFacturers.Remove(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }*/
    }
}
