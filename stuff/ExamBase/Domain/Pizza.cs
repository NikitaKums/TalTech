using System.Collections.Generic;
using Domain.Identity;

namespace Domain
{
    public class Pizza : BaseEntity
    {
        public string Description { get; set; }
        public int Pirce { get; set; }
        
        public ICollection<ToppingOnPizza> ToppingsOnPizza { get; set; }
        public ICollection<PizzaInOrder> PizzasInOrder { get; set; }
    }
}