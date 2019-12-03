namespace PublicApi.v1.DTO
{
    public class OrderWithProductCount : Order
    {
        public int ProductsInOrderCount { get; set; }
    }
}