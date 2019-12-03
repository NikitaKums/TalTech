namespace BLL.App.DTO
{
    public class OrderReceived
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int ProductInOrderId { get; set; }
        public int Quantity { get; set; }
    }
}