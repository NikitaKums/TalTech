using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO
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
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string ProductName { get; set; }
        
        public int ReturnId { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string ReturnDescription { get; set; }
    }
}