using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using DAL.App.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using ee.itcollege.nikita.Identity;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class ProductsReturnedController : Controller
    {
        private readonly IAppBLL _bll;

        public ProductsReturnedController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: ProductsReturned
        /*public async Task<IActionResult> Index()
        {
            var productReturned = await _bll.ProductsReturned.AllAsyncByShop(User.GetShopId());
            return View(productReturned);
        }

        // GET: ProductsReturned/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productReturned = await _bll.ProductsReturned.FindAsync(id);
            if (productReturned == null)
            {
                return NotFound();
            }

            return View(productReturned);
        }*/

        // GET: ProductsReturned/Create
        public async Task<IActionResult> Create(int? id)
        {
            var vm = new ProductReturnedCreateViewModel()
            {
                ProductSelectList = new SelectList(await _bll.Products.AllAsyncByShopForDropDown(User.GetShopId()), nameof(Product.Id), nameof(Product.ProductName)),
                ReturnSelectList = new SelectList(await _bll.Returns.AllAsyncByShop(User.GetShopId(), null, null, null, null), nameof(Return.Id), nameof(Return.Description))
            };
            
            foreach (var @return in vm.ReturnSelectList)
            {
                if (!@return.Value.Equals(id.ToString())) continue;
                @return.Selected = true;
                break;
            }
            
            return View(vm);
        }

        // POST: ProductsReturned/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductReturnedCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _bll.ProductsReturned.AddAsync(vm.ProductReturned);
                await _bll.SaveChangesAsync();
                return RedirectToAction("Index", "Returns");
            }
            vm.ProductSelectList = new SelectList(await _bll.Products.AllAsyncByShopForDropDown(User.GetShopId()), nameof(Product.Id), nameof(Product.ProductName));
            vm.ReturnSelectList = new SelectList(await _bll.Returns.AllAsyncByShop(User.GetShopId(), null, null, null, null), nameof(Return.Id), nameof(Return.Description));
            
            return View(vm);
        }

        // GET: ProductsReturned/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productReturned = await _bll.ProductsReturned.FindAsync(id);
            if (productReturned == null)
            {
                return NotFound();
            }
            
            var vm = new ProductReturnedCreateViewModel()
            {
                ProductReturned = productReturned,
                ProductSelectList = new SelectList(await _bll.Products.AllAsyncByShopForDropDown(User.GetShopId()), nameof(Product.Id), nameof(Product.ProductName)),
                ReturnSelectList = new SelectList(await _bll.Returns.AllAsyncByShop(User.GetShopId(), null, null, null, null), nameof(Return.Id), nameof(Return.Description))
            };
            
            return View(vm);
        }

        // POST: ProductsReturned/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductReturnedCreateViewModel vm)
        {
            if (id != vm.ProductReturned.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.ProductsReturned.Update(vm.ProductReturned);
                await _bll.SaveChangesAsync();
                return RedirectToAction("Details", "Returns", new {id = vm.ProductReturned.ReturnId});
            }
            vm.ProductSelectList = new SelectList(await _bll.Products.AllAsyncByShopForDropDown(User.GetShopId()), nameof(Product.Id), nameof(Product.ProductName));
            vm.ReturnSelectList = new SelectList(await _bll.Returns.AllAsyncByShop(User.GetShopId(), null, null, null, null), nameof(Return.Id), nameof(Return.Description));
            
            return View(vm);
        }

        // GET: ProductsReturned/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productReturned = await _bll.ProductsReturned.FindAsync(id);
            if (productReturned == null)
            {
                return NotFound();
            }

            return View(productReturned);
        }

        // POST: ProductsReturned/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productReturned = await _bll.ProductsReturned.FindAsync(id);
            
            _bll.ProductsReturned.Remove(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction("Details", "Returns", new {id = productReturned.ReturnId});
        }

    }
}
