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
    public class OrdersController : Controller
    {
        private readonly IAppBLL _bll;

        public OrdersController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Orders
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DescriptionSortParam"] = string.IsNullOrEmpty(sortOrder) ? "description_desc" : "";
            ViewData["CreationTimeSortParam"] = sortOrder == "createdAt" ? "createdAt_desc" : "createdAt";
            ViewData["ShipperSortParam"] = sortOrder == "shipper" ? "shipper_desc" : "shipper";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var orders = await _bll.Orders.AllAsync(sortOrder, searchString, pageNumber ?? 1, 10);

            return View(PaginatedList<BLL.App.DTO.DomainLikeDTO.Order>.Create(orders, pageNumber ?? 1, 10, 
                await _bll.Orders.CountDataAmount(searchString)));
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _bll.Orders.FindAsync(id);


            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            var vm = new OrderCreateViewModel()
            {
                ShipperSelectList = new SelectList(await _bll.Shippers.AllAsync(), 
                    nameof(Shipper.Id), nameof(Shipper.ShipperName)),
                ShopSelectList = new SelectList(await _bll.Shops.AllAsync(), 
                    nameof(Shop.Id), nameof(Shop.ShopName))
            };
            
            return View(vm);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _bll.Orders.AddAsync(vm.Order);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            vm.ShipperSelectList = new SelectList(await _bll.Shippers.AllAsync(), 
                nameof(Shipper.Id), nameof(Shipper.ShipperName));

            vm.ShopSelectList = new SelectList(await _bll.Shops.AllAsync(), 
                nameof(Shop.Id), nameof(Shop.ShopName));
            return View(vm);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var order = await _bll.Orders.FindAsync(id);
            
            if (order == null)
            {
                return NotFound();
            }
            
            var vm = new OrderCreateViewModel()
            {
                Order = order,
                ShipperSelectList = new SelectList(await _bll.Shippers.AllAsync(), 
                    nameof(Shipper.Id), nameof(Shipper.ShipperName)),
                ShopSelectList = new SelectList(await _bll.Shops.AllAsync(), 
                    nameof(Shop.Id), nameof(Shop.ShopName))
            };

            return View(vm);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderCreateViewModel vm)
        {
            if (id != vm.Order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Orders.Update(vm.Order);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            vm.ShipperSelectList = new SelectList(await _bll.Shippers.AllAsync(), 
                nameof(Shipper.Id), nameof(Shipper.ShipperName));

            vm.ShopSelectList = new SelectList(await _bll.Shops.AllAsync(), 
                nameof(Shop.Id), nameof(Shop.ShopName));
            return View(vm);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _bll.Orders.FindAsync(id);

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
            await _bll.Orders.DeleteOrderWithProducts(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> OrderReceived(int id)
        {
            var order = await _bll.Orders.FindAsyncById(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        
        public async Task<IActionResult> ProcessReceivedOrder(int id)
        {
            await _bll.Orders.ProcessReceivedOrder(id);
            return RedirectToAction(nameof(Index));
        }

        public RedirectToActionResult AddProductToOrder(int id)
        {
            return RedirectToAction(nameof(Create), "ProductsInOrder", new {id = id});
        }
        
    }

}
