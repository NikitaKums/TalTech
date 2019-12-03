using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO
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
        public string ProductName { get; set; }
        
        public int OrderId { get; set; }
        public string OrderDescription { get; set; }
    }
}