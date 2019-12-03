using System.Collections.Generic;

namespace DAL.App.DTO
{
    public class Delivery : BaseEntity
    {
        public string DeliveryService { get; set; }
        public string Description { get; set; }
        public int DeliveryPrice { get; set; }
        
        public List<Order> OrdersInDelivery { get; set; }
    }
}