using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class SaleCreateViewModel
    {
        public BLL.App.DTO.DomainLikeDTO.Sale Sale { get; set; }
        
        public SelectList AppUserSelectList { get; set; }
    }
}