using System.Collections.Generic;

namespace Domain
{
    public class Delivery : BaseEntity
    {
        public string DeliveryService { get; set; }
        public string Description { get; set; }
        public int DeliveryPrice { get; set; }
        
        public ICollection<Order> OrdersInDelivery { get; set; }
    }
}