using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using ee.itcollege.nikita.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Areas.ShopManager.Controllers
{
    [Authorize(Roles = "ShopManager, Admin")]
    [Area("ShopManager")]
    public class HomeController : Controller
    {
        private readonly IAppBLL _bll;

        public HomeController(IAppBLL bll)
        {
            _bll = bll;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new HomeCreateViewModel
            {
                ProductCount = await _bll.Products.CountProductsInShop(User.GetShopId()),
                UserCount = await _bll.AppUsers.CountUsersInShop(User.GetShopId()),
                OrderCount = await _bll.Orders.CountOrdersInShop(User.GetShopId())
            };
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}