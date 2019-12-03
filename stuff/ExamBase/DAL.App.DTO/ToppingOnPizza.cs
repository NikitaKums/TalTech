namespace DAL.App.DTO
{
    public class ToppingOnPizza
    {
        public int Id { get; set; }
        
        public int PizzaId { get; set; }
        public string PizzaDescription { get; set; }

        public int ToppingId { get; set; }
        public string ToppingDescription { get; set; }
    }
}