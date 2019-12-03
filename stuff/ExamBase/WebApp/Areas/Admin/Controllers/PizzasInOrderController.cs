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
    public class PizzasInOrderController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public PizzasInOrderController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /*// GET: PizzasInOrder
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.PizzasInOrder.AllAsyncWithSearch(User.GetUserId()));
        }*/

        // GET: PizzasInOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaInOrder = await _unitOfWork.PizzasInOrder.FindAsyncById(id, User.GetUserId());
            if (pizzaInOrder == null)
            {
                return NotFound();
            }

            return View(pizzaInOrder);
        }

        // GET: PizzasInOrder/Create
        public async Task<IActionResult> Create(int? id)
        {
            var vm = new PizzaInOrderVM
            {
                PizzaSelectList = new SelectList(await _unitOfWork.Pizzas.AllAsyncWithSearch(null), nameof(Pizza.Id),
                    nameof(Pizza.Description)),
                OrderSelectList = new SelectList(await _unitOfWork.Orders.AllAsync(), nameof(Order.Id),
                    nameof(Order.Description))
            };
            
            foreach (var pizzaInOrder in vm.OrderSelectList)
            {
                if (!pizzaInOrder.Value.Equals(id.ToString())) continue;
                pizzaInOrder.Selected = true;
                break;
            }
            
            return View(vm);
        }

        // POST: PizzasInOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PizzaInOrderVM vm)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.PizzasInOrder.AddAsync(vm.PizzaInOrder);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index", "Orders");
            }

            vm.PizzaSelectList = new SelectList(await _unitOfWork.Pizzas.AllAsyncWithSearch(null), nameof(Drink.Id),
                nameof(Drink.Description));
            vm.OrderSelectList = new SelectList(await _unitOfWork.Orders.AllAsync(), nameof(Order.Id),
                nameof(Order.Description));
            return View(vm);
        }

        // GET: PizzasInOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaInOrder = await _unitOfWork.PizzasInOrder.FindAsyncById(id, User.GetUserId());
            if (pizzaInOrder == null)
            {
                return NotFound();
            }

            var vm = new PizzaInOrderVM
            {
                PizzaInOrder = pizzaInOrder,
                PizzaSelectList = new SelectList(await _unitOfWork.Pizzas.AllAsyncWithSearch(null), nameof(Pizza.Id),
                    nameof(Pizza.Description)),
                OrderSelectList = new SelectList(await _unitOfWork.Orders.AllAsync(), nameof(Order.Id),
                    nameof(Order.Description))
            };
            return View(vm);
        }

        // POST: PizzasInOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PizzaInOrderVM vm)
        {
            if (id != vm.PizzaInOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.PizzasInOrder.Update(vm.PizzaInOrder);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction("Index", "Orders");
            }

            vm.PizzaSelectList = new SelectList(await _unitOfWork.Pizzas.AllAsyncWithSearch(null), nameof(Drink.Id),
                nameof(Drink.Description));
            vm.OrderSelectList = new SelectList(await _unitOfWork.Orders.AllAsync(), nameof(Order.Id),
                nameof(Order.Description));
            return View(vm);
        }

        // GET: PizzasInOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaInOrder = await _unitOfWork.PizzasInOrder.FindAsyncById(id, User.GetUserId());
            if (pizzaInOrder == null)
            {
                return NotFound();
            }

            return View(pizzaInOrder);
        }

        // POST: PizzasInOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _unitOfWork.PizzasInOrder.Remove(id);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index", "Orders");
        }
    }
}