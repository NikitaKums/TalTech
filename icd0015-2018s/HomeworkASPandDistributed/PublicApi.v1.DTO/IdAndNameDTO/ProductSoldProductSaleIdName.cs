using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO.IdAndNameDTO
{
    public class ProductSoldProductSaleIdName
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        
        public int SaleId { get; set; }
        public string SaleDescription { get; set; }
    }
}