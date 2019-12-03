namespace Domain
{
    public class PizzaInOrder : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }
    }
}