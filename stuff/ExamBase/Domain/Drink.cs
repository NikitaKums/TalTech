using System.Collections.Generic;

namespace Domain
{
    public class Drink : BaseEntity
    {
        public string Description { get; set; }
        public int Pirce { get; set; }
        
        public ICollection<DrinkInOrder> DrinksInOrder { get; set; }
    }
}