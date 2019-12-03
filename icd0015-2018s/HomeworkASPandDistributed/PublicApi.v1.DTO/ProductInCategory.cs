namespace PublicApi.v1.DTO
{
    public class ProductInCategory
    {
        public int Id { get; set; }
        
        public int CategoryId { get; set; }
        
        public string CategoryName { get; set; }
        
        public int ProductId { get; set; }
        
        public string ProductName { get; set; }
    }
}