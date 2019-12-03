using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain;
using ee.itcollege.nikita.Identity;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Areas.ShopManager.Controllers
{
    [Authorize(Roles = "ShopManager, Admin")]
    [Area("ShopManager")]
    public class ProductsController : Controller
    {
        private readonly IAppBLL _bll;

        public ProductsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Products
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["ProductNameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "productName_desc" : "";
            ViewData["ManufacturerCodeSortParam"] = sortOrder == "manufacturerCode" ? "manufacturerCode_desc" : "manufacturerCode";
            ViewData["ShopCodeSortParam"] = sortOrder == "shopCode" ? "shopCode_desc" : "shopCode";
            ViewData["BuyPriceSortParam"] = sortOrder == "buyPrice" ? "buyPrice_desc" : "buyPrice";
            ViewData["SellPriceSortParam"] = sortOrder == "sellPrice" ? "sellPrice_desc" : "sellPrice";
            ViewData["PercentageSortParam"] = sortOrder == "percentage" ? "percentage_desc" : "percentage";
            ViewData["QuantitySortParam"] = sortOrder == "quantity" ? "quantity_desc" : "quantity";
            ViewData["WeightSortParam"] = sortOrder == "weight" ? "weight_desc" : "weight";
            ViewData["LengthSortParam"] = sortOrder == "length" ? "length_desc" : "length";
            ViewData["ManufacturerNameSortParam"] = sortOrder == "manufacturer" ? "manufacturer_desc" : "manufacturer";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            
            var product = await _bll.Products.AllAsyncByShop(User.GetShopId(), sortOrder, searchString, pageNumber ?? 1, 10);
            return View(PaginatedList<BLL.App.DTO.DomainLikeDTO.Product>.Create(product, pageNumber ?? 1, 10, 
                await _bll.Products.CountDataAmount(User.GetShopId(), searchString)));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _bll.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ProductCreateViewModel()
            {
                InventorySelectList = new SelectList(await _bll.Inventories.AllAsyncByShop(User.GetShopId(), null, null, null, null), nameof(Inventory.Id), nameof(Inventory.Description)),
                ManuFacturerSelectList = new SelectList(await _bll.ManuFacturers.AllAsync(), nameof(ManuFacturer.Id), nameof(ManuFacturer.ManuFacturerName)),
                ShopSelectList = new SelectList(await _bll.Shops.GetShopByUserShopIdForDropDown(User.GetShopId()), nameof(Shop.Id), nameof(Shop.ShopName))

            };
            
            return View(vm);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _bll.Products.AddAsync(vm.Product);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.InventorySelectList = new SelectList(await _bll.Inventories.AllAsyncByShop(User.GetShopId(), null, null, null, null), nameof(Inventory.Id), nameof(Inventory.Description));
            vm.ManuFacturerSelectList = new SelectList(await _bll.ManuFacturers.AllAsync(), nameof(ManuFacturer.Id),nameof(ManuFacturer.ManuFacturerName));
            vm.ShopSelectList = new SelectList(await _bll.Shops.GetShopByUserShopIdForDropDown(User.GetShopId()), nameof(Shop.Id), nameof(Shop.ShopName));
            return View(vm);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _bll.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            
            var vm = new ProductCreateViewModel()
            {
                Product = product,
                InventorySelectList = new SelectList(await _bll.Inventories.AllAsyncByShop(User.GetShopId(),null, null, null, null), nameof(Inventory.Id), nameof(Inventory.Description)),
                ManuFacturerSelectList = new SelectList(await _bll.ManuFacturers.AllAsync(), nameof(ManuFacturer.Id), nameof(ManuFacturer.ManuFacturerName)),
                ShopSelectList = new SelectList(await _bll.Shops.GetShopByUserShopIdForDropDown(User.GetShopId()), nameof(Shop.Id), nameof(Shop.ShopName))

            };

            return View(vm);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductCreateViewModel vm)
        {
            if (id != vm.Product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Products.Update(vm.Product);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }

            vm.InventorySelectList = new SelectList(await _bll.Inventories.AllAsyncByShop(User.GetShopId(), null, null, null, null), nameof(Inventory.Id),
                nameof(Inventory.Description));
            vm.ManuFacturerSelectList = new SelectList(await _bll.ManuFacturers.AllAsync(), nameof(ManuFacturer.Id),
                nameof(ManuFacturer.ManuFacturerName));
            vm.ShopSelectList = new SelectList(await _bll.Shops.GetShopByUserShopIdForDropDown(User.GetShopId()), nameof(Shop.Id), nameof(Shop.ShopName));
            
            return View(vm);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _bll.Products.FindAsync(id);


            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

       
        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _bll.Products.Remove(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
