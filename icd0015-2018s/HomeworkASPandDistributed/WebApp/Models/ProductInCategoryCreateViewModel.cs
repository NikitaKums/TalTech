using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class ProductInCategoryCreateViewModel
    {
        public BLL.App.DTO.DomainLikeDTO.ProductInCategory ProductInCategory { get; set; }
        
        public SelectList CategorySelectList { get; set; }
        public SelectList ProductSelectList { get; set; }
    }
}