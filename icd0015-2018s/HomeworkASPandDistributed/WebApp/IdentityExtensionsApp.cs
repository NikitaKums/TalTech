using System;
using System.Security.Claims;
using Domain.Identity;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace WebApp
{
    public static class IdentityExtensionsApp
    {
        public static int? GetShopId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            string shopId = principal.FindFirst(nameof(AppUser.ShopId)).Value;

            if (!int.TryParse(shopId, out int actualShopId))
            {
                return null;
            }

            return actualShopId;
        }
        
        public static void IdentityAddDefaultRolesAndUsers(this IApplicationBuilder app, UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            if (!roleManager.Roles.Any(r => r.NormalizedName == "ADMIN"))
            {
                var result = roleManager.CreateAsync(new AppRole() {Name = "Admin"}).Result;
                if (result == IdentityResult.Success)
                {
                }
            }
            
            if (!roleManager.Roles.Any(r => r.NormalizedName == "SHOPMANAGER"))
            {
                var result = roleManager.CreateAsync(new AppRole() {Name = "ShopManager"}).Result;
                if (result == IdentityResult.Success)
                {
                }
            }

            var user = userManager.FindByEmailAsync("admin@admin.com").Result;
            if (user == null)
            {
                var result = userManager.CreateAsync(new AppUser()
                {
                    Email = "admin@admin.com",
                    UserName = "admin@admin.com",
                    FirstName = "AdminName",
                    LastName = "AdminLastName"
                }, "Password@7777777lol").Result;
                if (result == IdentityResult.Success)
                {
                    user = userManager.FindByEmailAsync("admin@admin.com").Result;
                }
            }

            if (user != null && !userManager.IsInRoleAsync(user, "Admin").Result)
            {
                var result = userManager.AddToRoleAsync(user, "Admin").Result;
            }
            
        }
    }
}