using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class ReturnCreateViewModel
    {
        public BLL.App.DTO.DomainLikeDTO.Return Return { get; set; }
        
        public SelectList ShopSelectList { get; set; }
    }
}