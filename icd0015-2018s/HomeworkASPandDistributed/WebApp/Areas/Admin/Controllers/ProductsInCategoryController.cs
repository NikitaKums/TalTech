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
using DAL.App.EF.Repositories;
using Domain;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ProductsInCategoryController : Controller
    {
        private readonly IAppBLL _bll;

        public ProductsInCategoryController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: ProductsInCategory
        public async Task<IActionResult> Index()
        {
            var productInCategory = await _bll.ProductsInCategory.AllAsync();
            return View(productInCategory);
        }

        // GET: ProductsInCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInCategory = await _bll.ProductsInCategory.FindAsync(id);
            if (productInCategory == null)
            {
                return NotFound();
            }

            return View(productInCategory);
        }

        // GET: ProductsInCategory/Create
        public async Task<IActionResult> Create(int? id)
        {
            var vm = new ProductInCategoryCreateViewModel()
            {
                CategorySelectList = new SelectList(await _bll.Categories.AllAsync(), nameof(Category.Id), nameof(Category.CategoryName)),
                ProductSelectList = new SelectList(await _bll.Products.AllAsync(), nameof(Product.Id), nameof(Product.ProductName))
            };
            
            foreach (var category in vm.CategorySelectList)
            {
                if (!category.Value.Equals(id.ToString())) continue;
                category.Selected = true;
                break;
            }
            
            return View(vm);
        }

        // POST: ProductsInCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductInCategoryCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _bll.ProductsInCategory.AddAsync(vm.ProductInCategory);
                await _bll.SaveChangesAsync();
                return RedirectToAction("Index", "Categories");
            }

            vm.CategorySelectList = new SelectList(await _bll.Categories.AllAsync(), nameof(Category.Id),
                nameof(Category.CategoryName));
            vm.ProductSelectList = new SelectList(await _bll.Products.AllAsync(), nameof(Product.Id),
                nameof(Product.ProductName));
            
            return View(vm);
        }

        // GET: ProductsInCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInCategory = await _bll.ProductsInCategory.FindAsync(id);
            if (productInCategory == null)
            {
                return NotFound();
            }
            var vm = new ProductInCategoryCreateViewModel()
            {
                ProductInCategory = productInCategory,
                CategorySelectList = new SelectList(await _bll.Categories.AllAsync(), nameof(Category.Id),
                    nameof(Category.CategoryName)),
                ProductSelectList = new SelectList(await _bll.Products.AllAsync(), nameof(Product.Id),
                    nameof(Product.ProductName))
            };
            return View(vm);
        }

        // POST: ProductsInCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductInCategoryCreateViewModel vm)
        {
            if (id != vm.ProductInCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.ProductsInCategory.Update(vm.ProductInCategory);
                await _bll.SaveChangesAsync();
                return RedirectToAction("Details", "Categories", new {id = vm.ProductInCategory.CategoryId});
            }

            vm.CategorySelectList = new SelectList(await _bll.Categories.AllAsync(), nameof(Category.Id),
            nameof(Category.CategoryName));
             vm.ProductSelectList = new SelectList(await _bll.Products.AllAsync(), nameof(Product.Id),
            nameof(Product.ProductName));
            return View(vm);
        }

        // GET: ProductsInCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInCategory = await _bll.ProductsInCategory.FindAsync(id);
            if (productInCategory == null)
            {
                return NotFound();
            }

            return View(productInCategory);
        }

        // POST: ProductsInCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productInCategory = await _bll.ProductsInCategory.FindAsync(id);

            _bll.ProductsInCategory.Remove(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction("Details", "Categories", new {id = productInCategory.CategoryId});
        }

    }
}
