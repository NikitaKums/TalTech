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
using ee.itcollege.nikita.Identity;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public OrdersController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string search)
        {
            ViewBag.search = search;
            var res = await _unitOfWork.Orders.AllAsyncWithSearch(search);
            foreach (var entry in res)
            {
                var price = 0;
                price += entry.DrinksInOrder.Sum(e => e.Drink.Pirce);
                price += entry.PizzasInOrder.Sum(e => e.Pizza.Pirce);
                entry.Price += price + entry.Delivery.DeliveryPrice;
            }
            return View(res);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _unitOfWork.Orders.FindAsyncById(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            var vm = new OrderVM
            {
                DeliverySelectList = new SelectList(await _unitOfWork.Deliverys.AllAsyncWithSearch(null),
                    nameof(Delivery.Id), nameof(Delivery.DeliveryService))
            };

            return View(vm);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderVM vm)
        {
            vm.Order.OrderState = OrderState.Waiting;
            vm.Order.Price = 0;
            if (ModelState.IsValid)
            {
                vm.Order.AppUserId = User.GetUserId();
                await _unitOfWork.Orders.AddAsync(vm.Order);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.DeliverySelectList = new SelectList(await _unitOfWork.Deliverys.AllAsyncWithSearch(null),
                nameof(Delivery.Id), nameof(Delivery.DeliveryService));

            return View(vm);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _unitOfWork.Orders.FindAsyncById(id);
            if (order == null)
            {
                return NotFound();
            }

            var vm = new OrderVM()
            {
                Order = order,
                DeliverySelectList = new SelectList(await _unitOfWork.Deliverys.AllAsyncWithSearch(null),
                    nameof(Delivery.Id), nameof(Delivery.DeliveryService))
            };
            return View(vm);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderVM vm)
        {
            if (id != vm.Order.Id)
            {
                return NotFound();
            }
            
            vm.Order.Price = await _unitOfWork.Orders.FindOrderPriceAsyncById(id, User.GetUserId());

            if (ModelState.IsValid)
            {
                vm.Order.AppUserId = User.GetUserId();
                _unitOfWork.Orders.Update(vm.Order);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            vm.DeliverySelectList = new SelectList(await _unitOfWork.Deliverys.AllAsyncWithSearch(null),
                nameof(Delivery.Id), nameof(Delivery.DeliveryService));
            return View(vm);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _unitOfWork.Orders.FindAsyncById(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _unitOfWork.Orders.Remove(id);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}