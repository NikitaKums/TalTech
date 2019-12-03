using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class OrderVM
    {
        public Order Order { get; set; }
        
        public SelectList DeliverySelectList { get; set; }
    }
}