using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class ProductSoldCreateViewModel
    {
        public BLL.App.DTO.DomainLikeDTO.ProductSold ProductSold { get; set; }
        
        public SelectList ProductSelectList { get; set; }
        public SelectList SaleSelectList { get; set; }
    }
}