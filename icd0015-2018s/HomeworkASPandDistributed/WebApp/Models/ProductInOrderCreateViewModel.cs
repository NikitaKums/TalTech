using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class ProductInOrderCreateViewModel
    {
        public BLL.App.DTO.DomainLikeDTO.ProductInOrder ProductInOrder { get; set; }
        
        public SelectList OrderSelectList { get; set; }
        public SelectList ProductSelectList { get; set; }
    }
}