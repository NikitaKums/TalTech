using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO
{
    public class ProductInOrder
    {
        public int Id { get; set; }
        
        [Range(1, int.MaxValue)]
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public DateTime ProductInOrderPlacingTime { get; set; }
        
        public int ProductId { get; set; }
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string ProductName { get; set; }
        
        public int OrderId { get; set; }
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string OrderDescription { get; set; }
    }
}