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
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")] 
    [Area("Admin")]
    public class ProductsWithDefectController : Controller
    {
        private readonly IAppBLL _bll;

        public ProductsWithDefectController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: ProductsWithDefect
        public async Task<IActionResult> Index()
        {
            var productWithDefect = await _bll.ProductsWithDefect.AllAsync();
            return View(productWithDefect);
        }

        // GET: ProductsWithDefect/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productWithDefect = await _bll.ProductsWithDefect.FindAsync(id);
            if (productWithDefect == null)
            {
                return NotFound();
            }

            return View(productWithDefect);
        }

        // GET: ProductsWithDefect/Create
        public async Task<IActionResult> Create(int? id)
        {
            var vm = new ProductWithDefectCreateViewModel()
            {
                DefectSelectList = new SelectList(await _bll.Defects.AllAsync(), nameof(Defect.Id), nameof(Defect.Description)),
                ProductSelectList = new SelectList(await _bll.Products.AllAsync(), nameof(Product.Id), nameof(Product.ProductName))
            };
            
            foreach (var defect in vm.DefectSelectList)
            {
                if (!defect.Value.Equals(id.ToString())) continue;
                defect.Selected = true;
                break;
            }
            
            return View(vm);
        }

        // POST: ProductsWithDefect/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductWithDefectCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _bll.ProductsWithDefect.AddAsync(vm.ProductWithDefect);
                await _bll.SaveChangesAsync();
                return RedirectToAction("Index", "Defects");
            }
            vm.DefectSelectList = new SelectList(await _bll.Defects.AllAsync(), nameof(Defect.Id), nameof(Defect.Description));
            vm.ProductSelectList = new SelectList(await _bll.Products.AllAsync(), nameof(Product.Id), nameof(Product.ProductName));
            
            return View(vm);
        }

        // GET: ProductsWithDefect/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productWithDefect = await _bll.ProductsWithDefect.FindAsync(id);
            if (productWithDefect == null)
            {
                return NotFound();
            }
            
            var vm = new ProductWithDefectCreateViewModel()
            {
                ProductWithDefect = productWithDefect,
                DefectSelectList =  new SelectList(await _bll.Defects.AllAsync(), nameof(Defect.Id), nameof(Defect.Description)),
                ProductSelectList = new SelectList(await _bll.Products.AllAsync(), nameof(Product.Id), nameof(Product.ProductName))
            };
            
            return View(vm);
        }

        // POST: ProductsWithDefect/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductWithDefectCreateViewModel vm)
        {
            if (id != vm.ProductWithDefect.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.ProductsWithDefect.Update(vm.ProductWithDefect);
                await _bll.SaveChangesAsync();
                return RedirectToAction("Details", "Defects", new {id = vm.ProductWithDefect.DefectId});
            }
            vm.DefectSelectList = new SelectList(await _bll.Defects.AllAsync(), nameof(Defect.Id), nameof(Defect.Description));
            vm.ProductSelectList = new SelectList(await _bll.Products.AllAsync(), nameof(Product.Id), nameof(Product.ProductName));
            
            return View(vm);
        }

        // GET: ProductsWithDefect/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productWithDefect = await _bll.ProductsWithDefect.FindAsync(id);
            if (productWithDefect == null)
            {
                return NotFound();
            }

            return View(productWithDefect);
        }

        // POST: ProductsWithDefect/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productWithDefect = await _bll.ProductsWithDefect.FindAsync(id);
            _bll.ProductsWithDefect.Remove(id);
            return RedirectToAction("Details", "Defects", new {id = productWithDefect.DefectId});
        }
    }
}
