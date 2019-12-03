using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class ProductReturnedCreateViewModel
    {
        public BLL.App.DTO.DomainLikeDTO.ProductReturned ProductReturned { get; set; }
        
        public SelectList ProductSelectList { get; set; }
        public SelectList ReturnSelectList { get; set; }
    }
}