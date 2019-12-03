using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO.IdAndNameDTO
{
    public class ProductSoldProductSaleIdName
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }
        
        [MaxLength(256)]
        [MinLength(1)]
        [Required]
        public string ProductName { get; set; }
        
        public int SaleId { get; set; }
        
        [MaxLength(256)]
        [MinLength(1)]
        [Required]
        public string SaleDescription { get; set; }
    }
}