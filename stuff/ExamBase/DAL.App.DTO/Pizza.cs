using System.Collections.Generic;

namespace DAL.App.DTO
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Pirce { get; set; }
        
        public List<ToppingOnPizza> ToppingsOnPizza { get; set; }
    }
}