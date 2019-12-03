using System.Collections.Generic;

namespace DAL.App.DTO
{
    public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string OrderState { get; set; }

        public int DeliveryId { get; set; }
        public string DeliveryService { get; set; }

        public List<PizzaInOrder> PizzasInOrder { get; set; }
        public List<DrinkInOrder> DrinksInOrder { get; set; }
    }
}