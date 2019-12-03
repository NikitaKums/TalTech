using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO
{
    public class ProductReturned
    {
        public int Id { get; set; }
        
        
        [Range(1, int.MaxValue)]
        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime ProductReturnedTime { get; set; }
        
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int ReturnId { get; set; }
        public string ReturnDescription { get; set; }
    }
}