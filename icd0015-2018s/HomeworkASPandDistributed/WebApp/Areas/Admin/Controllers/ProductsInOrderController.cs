using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")] 
    [Area("Admin")]
    public class ProductsInOrderController : Controller
    {
        private readonly IAppBLL _bll;

        public ProductsInOrderController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: ProductsInOrder
        public async Task<IActionResult> Index()
        {
            var productInOrder = await _bll.ProductsInOrder.AllAsync();
            return View(productInOrder);
        }

        // GET: ProductsInOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInOrder = await _bll.ProductsInOrder.FindAsync(id);
            if (productInOrder == null)
            {
                return NotFound();
            }

            return View(productInOrder);
        }

        // GET: ProductsInOrder/Create
        public async Task<IActionResult> Create(int? id)
        {
            var vm = new ProductInOrderCreateViewModel()
            {
                OrderSelectList = new SelectList(await _bll.Orders.AllAsync(), nameof(Order.Id), nameof(Order.Description)),
                ProductSelectList = new SelectList(await _bll.Products.AllAsync(), nameof(Product.Id), nameof(Product.ProductName))
            };
            
            foreach (var order in vm.OrderSelectList)
            {
                if (!order.Value.Equals(id.ToString())) continue;
                order.Selected = true;
                break;
            }

            return View(vm);
        }

        // POST: ProductsInOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductInOrderCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _bll.ProductsInOrder.AddAsync(vm.ProductInOrder);
                await _bll.SaveChangesAsync();
                return RedirectToAction("Index", "Orders");
            }
            vm.OrderSelectList = new SelectList(await _bll.Orders.AllAsync(), nameof(Order.Id), nameof(Order.Description));
            vm.ProductSelectList = new SelectList(await _bll.Products.AllAsync(), nameof(Product.Id), nameof(Product.ProductName));
            
            return View(vm);
        }

        // GET: ProductsInOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInOrder = await _bll.ProductsInOrder.FindAsync(id);
            if (productInOrder == null)
            {
                return NotFound();
            }
            var vm = new ProductInOrderCreateViewModel()
            {
                ProductInOrder = productInOrder,
                OrderSelectList = new SelectList(await _bll.Orders.AllAsync(), nameof(Order.Id), nameof(Order.Description)),
                ProductSelectList = new SelectList(await _bll.Products.AllAsync(), nameof(Product.Id), nameof(Product.ProductName))
            };
            
            return View(vm);
        }

        // POST: ProductsInOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductInOrderCreateViewModel vm)
        {
            if (id != vm.ProductInOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.ProductsInOrder.Update(vm.ProductInOrder);
                await _bll.SaveChangesAsync();
                return RedirectToAction("Details", "Orders", new {id = vm.ProductInOrder.OrderId});
            }
            
            vm.OrderSelectList = new SelectList(await _bll.Orders.AllAsync(), nameof(Order.Id), nameof(Order.Description));
            vm.ProductSelectList = new SelectList(await _bll.Products.AllAsync(), nameof(Product.Id), nameof(Product.ProductName));
            
            return View(vm);
        }

        // GET: ProductsInOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInOrder = await _bll.ProductsInOrder.FindAsync(id);
            if (productInOrder == null)
            {
                return NotFound();
            }

            return View(productInOrder);
        }

        // POST: ProductsInOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productInOrder = await _bll.ProductsInOrder.FindAsync(id);
            _bll.ProductsInOrder.Remove(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction("Details", "Orders", new {id = productInOrder.OrderId});
        }
        
        public async Task<IActionResult>  ProductInOrderReceived(int id)
        {
            await _bll.ProductsInOrder.ProductInOrderReceived(id);
            return RedirectToAction("Index", "Orders");
        }

    }
}
