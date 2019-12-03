using System.Collections.Generic;

namespace Domain
{
    public class Topping : BaseEntity
    {
        public string Description { get; set; }
        public int Pirce { get; set; }
        
        public ICollection<ToppingOnPizza> ToppingsOnPizza { get; set; }
    }
}