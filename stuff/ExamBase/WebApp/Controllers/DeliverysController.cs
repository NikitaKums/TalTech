using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class DeliverysController : Controller
    {
        private readonly IAppUnitOfWork  _unitOfWork;

        public DeliverysController(IAppUnitOfWork  unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Deliverys
        public async Task<IActionResult> Index(string search)
        {
            ViewBag.search = search;
            return View(await _unitOfWork.Deliverys.AllAsyncWithSearch(search));
        }

        // GET: Deliverys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _unitOfWork.Deliverys.FindAsyncById(id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // GET: Deliverys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Deliverys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliveryService,Description,DeliveryPrice,Id")] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Deliverys.AddAsync(delivery);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(delivery);
        }

        // GET: Deliverys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _unitOfWork.Deliverys.FindAsyncById(id);
            if (delivery == null)
            {
                return NotFound();
            }
            return View(delivery);
        }

        // POST: Deliverys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeliveryService,Description,DeliveryPrice,Id")] Delivery delivery)
        {
            if (id != delivery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                    _unitOfWork.Deliverys.Update(delivery);
                    await _unitOfWork.SaveChangesAsync();
                    
                return RedirectToAction(nameof(Index));
            }
            return View(delivery);
        }

        // GET: Deliverys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _unitOfWork.Deliverys.FindAsyncById(id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // POST: Deliverys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _unitOfWork.Deliverys.Remove(id);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
