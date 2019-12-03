namespace Domain
{
    public class DrinkInOrder : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int DrinkId { get; set; }
        public Drink Drink { get; set; }
    }
}