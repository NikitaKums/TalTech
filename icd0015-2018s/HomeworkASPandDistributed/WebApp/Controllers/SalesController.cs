using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain;
using Domain.Identity;
using ee.itcollege.nikita.Identity;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;


namespace WebApp.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly IAppBLL _bll;

        public SalesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Sales
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DescriptionSortParam"] = string.IsNullOrEmpty(sortOrder) ? "description_desc" : "";
            ViewData["CreationTimeSortParam"] = sortOrder == "createdAt" ? "createdAt_desc" : "createdAt";
            ViewData["UserSortParam"] = sortOrder == "user" ? "user_desc" : "user";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var sale = await _bll.Sales.AllAsyncByShop(User.GetShopId(), sortOrder, searchString, pageNumber ?? 1, 10);

            var saleAmounts = await _bll.Sales.GetSaleAmounts(User.GetShopId());
            return View(new SalesIndexModel()
            {
                Sales = PaginatedList<BLL.App.DTO.DomainLikeDTO.Sale>.Create(sale, pageNumber ?? 1, 10, 
                    await _bll.Sales.CountDataAmount(User.GetShopId(), searchString)),
                SaleAmounts = saleAmounts
            });
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var sale = await _context.Sales
                .Include(s => s.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);*/

            var sale = await _bll.Sales.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public async Task<IActionResult> Create()
        {
            var vm = new SaleCreateViewModel()
            {
                AppUserSelectList = new SelectList(await _bll.AppUsers.GetUserById(User.GetUserId()),
                    nameof(AppUser.Id), nameof(AppUser.FirstLastName))
            };
            return View(vm);
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleCreateViewModel vm)
        {
            if (ModelState.IsValid && vm.Sale.AppUserId == User.GetUserId())
            {
                await _bll.Sales.AddAsync(vm.Sale);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            vm.AppUserSelectList = new SelectList(await _bll.AppUsers.GetUserById(User.GetUserId()),
                nameof(AppUser.Id), nameof(AppUser.FirstLastName), vm.Sale.AppUserId);

            return View(vm);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _bll.Sales.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            var vm = new SaleCreateViewModel()
            {
                Sale = sale,
                AppUserSelectList = new SelectList(await _bll.AppUsers.GetUserById(User.GetUserId()),
                    nameof(AppUser.Id), nameof(AppUser.FirstLastName), sale.AppUserId)
            };

            return View(vm);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SaleCreateViewModel vm)
        {
            if (id != vm.Sale.Id)
            {
                return NotFound();
            }

            var sale = await _bll.Sales.FindAsync(id);
            
            if (User.GetUserId() != sale.AppUserId)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                _bll.Sales.Update(vm.Sale);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            vm.AppUserSelectList = new SelectList(await _bll.AppUsers.GetUserById(User.GetUserId()),
                nameof(AppUser.Id), nameof(AppUser.FirstLastName), vm.Sale.AppUserId);

            return View(vm);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _bll.Sales.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sale = await _bll.Sales.FindAsync(id);

            if (User.GetUserId() == sale.AppUserId)
            {
                await _bll.Sales.DeleteWithoutRestoration(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("DeleteWithRestore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWithRestore(int id)
        {
            var sale = await _bll.Sales.FindAsync(id);

            if (User.GetUserId() == sale.AppUserId)
            {
                await _bll.Sales.DeleteWithRestoration(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<RedirectToActionResult> AddProductToSale(int id)
        {
            var sale = await _bll.Sales.FindAsync(id);
            if (sale?.AppUserId != User.GetUserId())
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Create), "ProductsSold", new {id = id});
        }
    }
}