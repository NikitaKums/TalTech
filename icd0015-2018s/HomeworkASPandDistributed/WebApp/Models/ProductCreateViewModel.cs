using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class ProductCreateViewModel
    {
        public BLL.App.DTO.DomainLikeDTO.Product Product { get; set; }

        public SelectList InventorySelectList { get; set; }
        public SelectList ManuFacturerSelectList { get; set; }
        public SelectList ShopSelectList { get; set; }

    }
}