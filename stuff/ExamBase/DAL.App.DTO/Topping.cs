using System.Collections.Generic;

namespace DAL.App.DTO
{
    public class Topping
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Pirce { get; set; }
        
        public List<ToppingOnPizza> ToppingsOnPizza { get; set; }
    }
}