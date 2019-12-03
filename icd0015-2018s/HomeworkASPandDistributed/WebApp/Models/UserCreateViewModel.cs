using System.Collections.Generic;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class UserCreateViewModel
    {
        public BLL.App.DTO.DomainLikeDTO.Identity.AppUser AppUser { get; set; }
        
        public SelectList ShopsSelectList { get; set; }
        public SelectList RolesSelectList { get; set; }

        public int SalesCount { get; set; }
        
        public IList<string> AppUserRoles { get; set; }
        public List<string> SelectedRoles { get; set; }
    }
}