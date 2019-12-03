using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class OrderCreateViewModel
    {
        public BLL.App.DTO.DomainLikeDTO.Order Order { get; set; }
        public SelectList ShipperSelectList { get; set; }
        public SelectList ShopSelectList { get; set; }

    }
}