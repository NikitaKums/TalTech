namespace DAL.App.DTO
{
    public class PizzaInOrder
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string OrderDescription { get; set; }

        public int PizzaId { get; set; }
        public string PizzaDescription { get; set; }
    }
}