using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ProductSold : DomainEntity
    {      
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        
        [Required]
        public DateTime ProductSoldTime { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
                
        public int SaleId { get; set; }
        public Sale Sale { get; set; }
    }
}