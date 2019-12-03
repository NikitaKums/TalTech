namespace DAL.App.DTO
{
    public class DrinkInOrder : BaseEntity
    {
        
        public int OrderId { get; set; }
        public string OrderDescription { get; set; }

        public int DrinkId { get; set; }
        public string DrinkDescription { get; set; }
    }
}