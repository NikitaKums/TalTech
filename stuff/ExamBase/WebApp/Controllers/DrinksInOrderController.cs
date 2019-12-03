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

namespace WebApp.Controllers
{
    [Authorize]
    public class DrinksInOrderController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public DrinksInOrderController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /*// GET: DrinksInOrder
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.DrinksInOrder.AllAsyncByUserId(User.GetUserId()));
        }*/

        // GET: DrinksInOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkInOrder = await _unitOfWork.DrinksInOrder.FindAsyncById(id, User.GetUserId());
            if (drinkInOrder == null)
            {
                return NotFound();
            }
            return View(drinkInOrder);
        }

        // GET: DrinksInOrder/Create
        public async Task<IActionResult> Create(int? id)
        {
            var vm = new DrinkInOrderVM
            {
                DrinkSelectList = new SelectList(await _unitOfWork.Drinks.AllAsync(), nameof(Drink.Id),
                    nameof(Drink.Description)),
                OrderSelectList = new SelectList(await _unitOfWork.Orders.AllAsync(), nameof(Order.Id),
                    nameof(Order.Description))
            };

            foreach (var drinkInOrder in vm.OrderSelectList)
            {
                if (!drinkInOrder.Value.Equals(id.ToString())) continue;
                drinkInOrder.Selected = true;
                break;
            }
            
            return View(vm);
        }

        // POST: DrinksInOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DrinkInOrderVM vm)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DrinksInOrder.Add(vm.DrinkInOrder);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index", "Orders");
            }

            vm.DrinkSelectList = new SelectList(await _unitOfWork.Drinks.AllAsync(), nameof(Drink.Id),
                nameof(Drink.Description));
            vm.OrderSelectList = new SelectList(await _unitOfWork.Orders.AllAsync(), nameof(Order.Id),
                nameof(Order.Description));
            return View(vm);
        }

        // GET: DrinksInOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkInOrder = await _unitOfWork.DrinksInOrder.FindAsyncById(id, User.GetUserId());
            if (drinkInOrder == null)
            {
                return NotFound();
            }

            var vm = new DrinkInOrderVM
            {
                DrinkInOrder = drinkInOrder,
                DrinkSelectList = new SelectList(await _unitOfWork.Drinks.AllAsync(), nameof(Drink.Id),
                    nameof(Drink.Description)),
                OrderSelectList = new SelectList(await _unitOfWork.Orders.AllAsync(), nameof(Order.Id),
                    nameof(Order.Description))
            };
            return View(vm);
        }

        // POST: DrinksInOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DrinkInOrderVM vm)
        {
            if (id != vm.DrinkInOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.DrinksInOrder.Update(vm.DrinkInOrder);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction("Index", "Orders");
            }

            vm.DrinkSelectList = new SelectList(await _unitOfWork.Drinks.AllAsync(), nameof(Drink.Id),
                nameof(Drink.Description));
            vm.OrderSelectList = new SelectList(await _unitOfWork.Orders.AllAsync(), nameof(Order.Id),
                nameof(Order.Description));
            return View(vm);
        }

        // GET: DrinksInOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkInOrder = await _unitOfWork.DrinksInOrder.FindAsyncById(id, User.GetUserId());
            if (drinkInOrder == null)
            {
                return NotFound();
            }

            return View(drinkInOrder);
        }

        // POST: DrinksInOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _unitOfWork.DrinksInOrder.Remove(id);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index", "Orders");
        }
    }
}