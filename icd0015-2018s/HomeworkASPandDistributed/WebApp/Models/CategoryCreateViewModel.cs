using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class CategoryCreateViewModel
    {
        public BLL.App.DTO.DomainLikeDTO.Category Category { get; set; }
        public SelectList ShopSelectList { get; set; }
    }
}