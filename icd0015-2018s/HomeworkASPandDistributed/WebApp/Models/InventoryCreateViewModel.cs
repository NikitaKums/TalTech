using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class InventoryCreateViewModel
    {
        public BLL.App.DTO.DomainLikeDTO.Inventory Inventory { get; set; }
        public SelectList ShopSelectList { get; set; }
    }
}