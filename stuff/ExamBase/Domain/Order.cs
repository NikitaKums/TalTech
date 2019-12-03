using System.Collections.Generic;
using Domain.Identity;

namespace Domain
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public int Price { get; set; }
        public OrderState OrderState { get; set; }

        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
        
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        
        public ICollection<PizzaInOrder> PizzasInOrder { get; set; }
        public ICollection<DrinkInOrder> DrinksInOrder { get; set; }
    }
}