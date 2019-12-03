using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Domain;
using Domain.Identity;
using ee.itcollege.nikita.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UsersController(IAppBLL bll, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _bll = bll;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        // GET: Users
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["LastNameSortParam"] = sortOrder == "lastname" ? "lastname_desc" : "lastname";
            ViewData["EmailSortParam"] = sortOrder == "email" ? "email_desc" : "email";
            ViewData["AddressSortParam"] = sortOrder == "address" ? "address_desc" : "address";
            ViewData["ShopSortParam"] = sortOrder == "shop" ? "shop_desc" : "shop";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            
            var users = await _bll.AppUsers.AllAsync(sortOrder, searchString);
            
            var allUsersAndRoles = new List<UserCreateViewModel>();

            foreach (var user in users)
            {
                var tempUser = DAL.App.EF.Mappers.AppUserMapper.MapFromDAL(AppUserMapper.MapFromBLL(user));

                allUsersAndRoles.Add(new UserCreateViewModel()
                {
                    AppUser = user,
                    AppUserRoles = await _userManager.GetRolesAsync(tempUser)
                });
            }
                
            return View(PaginatedList<UserCreateViewModel>.Create(allUsersAndRoles, pageNumber ?? 1, 10, null));
        }
        
        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _bll.AppUsers.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            
            var vm = new UserCreateViewModel()
            {
                AppUser = AppUserMapper.MapFromDAL(DAL.App.EF.Mappers.AppUserMapper.MapFromDomain(user)),
                AppUserRoles = await _userManager.GetRolesAsync(user),
                ShopsSelectList = new SelectList(await _bll.Shops.AllAsync(), nameof(Shop.Id), nameof(Shop.ShopName), user.ShopId),
                RolesSelectList = new SelectList(await _roleManager.Roles.ToListAsync(), nameof(AppRole.Name), nameof(AppRole.Name))
            };

            PreSelect(vm.AppUserRoles, vm.RolesSelectList);
            
            return View(vm);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserCreateViewModel vm)
        {
            if (id != vm.AppUser.Id)
            {
                return NotFound();
            }
            var roles = await _userManager.GetRolesAsync(DAL.App.EF.Mappers.AppUserMapper.MapFromDAL(AppUserMapper.MapFromBLL(vm.AppUser)));
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (id != User.GetUserId())
            {
                await _userManager.RemoveFromRolesAsync(user, roles.ToArray());

                if (vm.SelectedRoles != null && id != User.GetUserId())
                {
                    foreach (var role in vm.SelectedRoles)
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }
                }
            }

            user.FirstName = vm.AppUser.FirstName;
            user.LastName = vm.AppUser.LastName;
            user.ShopId = vm.AppUser.ShopId;
            user.Aadress = vm.AppUser.Aadress;

            if (ModelState.IsValid)
            {
                await _userManager.UpdateAsync(user);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.ShopsSelectList = new SelectList(await _bll.Shops.AllAsync(), nameof(Shop.Id), nameof(Shop.ShopName),
                vm.AppUser.ShopId);
            vm.RolesSelectList = new SelectList(await _roleManager.Roles.ToListAsync(), nameof(AppRole.Name), nameof(AppRole.Name));
            
            PreSelect(vm.AppUserRoles, vm.RolesSelectList);

            return View();
        }

        public void PreSelect(IList<string> appUserRoles, SelectList rolesSelectList)
        {
            foreach (var role in rolesSelectList)
            {
                if (appUserRoles.Contains(role.Value))
                {
                    role.Selected = true;
                }
            }
        }
    }
}