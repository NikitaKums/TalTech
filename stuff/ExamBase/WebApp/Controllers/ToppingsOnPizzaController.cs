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
    public class ToppingsOnPizzaController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public ToppingsOnPizzaController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: ToppingsOnPizza
        /*public async Task<IActionResult> Index(string search)
        {
            ViewBag.search = search;
            return View(await _unitOfWork.ToppingsOnPizza.AllAsyncWithSearch(User.GetUserId(), search));
        }*/

        // GET: ToppingsOnPizza/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toppingOnPizza = await _unitOfWork.ToppingsOnPizza.FindAsyncById(id, User.GetUserId());
            if (toppingOnPizza == null)
            {
                return NotFound();
            }

            return View(toppingOnPizza);
        }

        // GET: ToppingsOnPizza/Create
        public async Task<IActionResult> Create(int? id)
        {
            var vm = new ToppingOnPizzaVM()
            {
                PizzaSelectList = new SelectList(await _unitOfWork.Pizzas.AllAsyncWithSearch(null), nameof(Pizza.Id),
                    nameof(Pizza.Description)),
                ToppingSelectList = new SelectList(await _unitOfWork.Toppings.AllAsyncWithSearch(null),
                    nameof(Topping.Id),
                    nameof(Topping.Description))
            };
            
            foreach (var toppingSelectList in vm.ToppingSelectList)
            {
                if (!toppingSelectList.Value.Equals(id.ToString())) continue;
                toppingSelectList.Selected = true;
                break;
            }

            return View(vm);
        }

        // POST: ToppingsOnPizza/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToppingOnPizzaVM vm)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.ToppingsOnPizza.AddAsync(vm.ToppingOnPizza);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index", "Pizzas");
            }

            vm.PizzaSelectList = new SelectList(await _unitOfWork.Pizzas.AllAsyncWithSearch(null), nameof(Pizza.Id),
                nameof(Pizza.Description));
            vm.ToppingSelectList = new SelectList(await _unitOfWork.Toppings.AllAsyncWithSearch(null),
                nameof(Topping.Id),
                nameof(Topping.Description));
            return View(vm);
        }

        // GET: ToppingsOnPizza/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toppingOnPizza = await _unitOfWork.ToppingsOnPizza.FindAsyncById(id, User.GetUserId());
            if (toppingOnPizza == null)
            {
                return NotFound();
            }

            var vm = new ToppingOnPizzaVM()
            {
                ToppingOnPizza = toppingOnPizza,
                PizzaSelectList = new SelectList(await _unitOfWork.Pizzas.AllAsyncWithSearch(null), nameof(Pizza.Id),
                    nameof(Pizza.Description)),
                ToppingSelectList = new SelectList(await _unitOfWork.Toppings.AllAsyncWithSearch(null),
                    nameof(Topping.Id),
                    nameof(Topping.Description))
            };
            return View(vm);
        }

        // POST: ToppingsOnPizza/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ToppingOnPizzaVM vm)
        {
            if (id != vm.ToppingOnPizza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.ToppingsOnPizza.Update(vm.ToppingOnPizza);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction("Index", "Pizzas");
            }

            vm.PizzaSelectList = new SelectList(await _unitOfWork.Pizzas.AllAsyncWithSearch(null), nameof(Pizza.Id),
                nameof(Pizza.Description));
            vm.ToppingSelectList = new SelectList(await _unitOfWork.Toppings.AllAsyncWithSearch(null),
                nameof(Topping.Id),
                nameof(Topping.Description));
            return View(vm);
        }

        // GET: ToppingsOnPizza/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toppingOnPizza = await _unitOfWork.ToppingsOnPizza.FindAsyncById(id, User.GetUserId());

            if (toppingOnPizza == null)
            {
                return NotFound();
            }

            return View(toppingOnPizza);
        }

        // POST: ToppingsOnPizza/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _unitOfWork.ToppingsOnPizza.Remove(id);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index", "Pizzas");
        }
    }
}