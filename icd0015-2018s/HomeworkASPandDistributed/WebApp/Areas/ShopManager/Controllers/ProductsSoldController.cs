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

namespace WebApp.Areas.ShopManager.Controllers
{
    [Authorize(Roles = "ShopManager, Admin")]
    [Area("ShopManager")]
    public class ProductsSoldController : Controller
    {
        private readonly IAppBLL _bll;

        public ProductsSoldController(IAppBLL bll)
        {
            _bll = bll;
        }


        /* // GET: ProductsSold
         public async Task<IActionResult> Index()
         {
             var productSold = await _bll.ProductsSold.AllAsyncByShop(User.GetShopId());
             return View(productSold);
         }
 
         // GET: ProductsSold/Details/5
         public async Task<IActionResult> Details(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }
 
             var productSold = await _bll.ProductsSold.FindAsync(id);
             if (productSold == null)
             {
                 return NotFound();
             }
 
             return View(productSold);
         }*/

        // GET: ProductsSold/Create
        public async Task<IActionResult> Create(int? id)
        {
            var vm = new ProductSoldCreateViewModel()
            {
                ProductSelectList = new SelectList(await _bll.Products.AllAsyncByShopAndInInventory(User.GetShopId()),
                    nameof(Product.Id), nameof(Product.ProductName)),
                SaleSelectList =
                    new SelectList(await _bll.Sales.AllAsyncByShopAndUserId(User.GetShopId(), User.GetUserId()),
                        nameof(Sale.Id), nameof(Sale.Description))
            };

            foreach (var sale in vm.SaleSelectList)
            {
                if (!sale.Value.Equals(id.ToString())) continue;
                sale.Selected = true;
                break;
            }

            return View(vm);
        }

        // POST: ProductsSold/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductSoldCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (await _bll.ProductsSold.AddProductToSale(vm.ProductSold.ProductId, vm.ProductSold))
                {
                    return RedirectToAction("Index", "Sales");
                }

                var product = await _bll.Products.FindAsync(vm.ProductSold.ProductId);
                ModelState.AddModelError(string.Empty,
                    "Cannot sell more than in stock! Current stock: " + product.Quantity);
            }

            vm.ProductSelectList = new SelectList(await _bll.Products.AllAsyncByShopAndInInventory(User.GetShopId()),
                nameof(Product.Id), nameof(Product.ProductName));
            vm.SaleSelectList =
                new SelectList(await _bll.Sales.AllAsyncByShopAndUserId(User.GetShopId(), User.GetUserId()),
                    nameof(Sale.Id), nameof(Sale.Description));

            return View(vm);
        }

        // GET: ProductsSold/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSold = await _bll.ProductsSold.FindAsync(id);
            if (productSold == null)
            {
                return NotFound();
            }

            var vm = new ProductSoldCreateViewModel()
            {
                ProductSold = productSold,
                ProductSelectList = new SelectList(await _bll.Products.AllAsyncByShopAndInInventory(User.GetShopId()),
                    nameof(Product.Id), nameof(Product.ProductName)),
                SaleSelectList =
                    new SelectList(await _bll.Sales.AllAsyncByShopAndUserId(User.GetShopId(), User.GetUserId()),
                        nameof(Sale.Id), nameof(Sale.Description))
            };

            return View(vm);
        }

        // POST: ProductsSold/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductSoldCreateViewModel vm)
        {
            if (id != vm.ProductSold.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await _bll.ProductsSold.EditProductInSale(id, vm.ProductSold.ProductId, vm.ProductSold))
                {
                    return RedirectToAction("Details", "Sales", new {id = vm.ProductSold.SaleId});
                }

                var product = await _bll.Products.FindAsync(vm.ProductSold.ProductId);
                ModelState.AddModelError(string.Empty, "Cannot sell more than in stock! Current stock: " + product.Quantity);
            }

            vm.ProductSelectList = new SelectList(await _bll.Products.AllAsyncByShopAndInInventory(User.GetShopId()),
                nameof(Product.Id), nameof(Product.ProductName));
            vm.SaleSelectList =
                new SelectList(await _bll.Sales.AllAsyncByShopAndUserId(User.GetShopId(), User.GetUserId()),
                    nameof(Sale.Id), nameof(Sale.Description));

            return View(vm);
        }

        // GET: ProductsSold/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSold = await _bll.ProductsSold.FindAsync(id);
            if (productSold == null)
            {
                return NotFound();
            }

            return View(productSold);
        }

        // POST: ProductsSold/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productSold = await _bll.ProductsSold.FindAsync(id);
            _bll.ProductsSold.Remove(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction("Details", "Sales", new {id = productSold.SaleId});
        }


        // POST: ProductsSold/Delete/5
        [HttpPost, ActionName("DeleteRestoreProductQuantity")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedRestoreProductQuantity(int id)
        {
            await _bll.ProductsSold.DeleteWithRestoration(id);
            return RedirectToAction("Index", "Sales");
        }
    }
}
