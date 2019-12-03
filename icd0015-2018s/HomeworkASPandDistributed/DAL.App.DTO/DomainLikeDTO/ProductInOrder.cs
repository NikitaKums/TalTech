using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO.DomainLikeDTO
{
    public class ProductInOrder : DomainEntity
    {
        [Range(1, int.MaxValue)]
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public DateTime ProductInOrderPlacingTime { get; set; }
    
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}