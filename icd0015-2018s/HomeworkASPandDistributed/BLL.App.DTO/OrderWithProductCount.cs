using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO
{
    public class OrderWithProductCount
    {
        public int Id { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string Description { get; set; }
        
        [Required]
        public DateTime OrderCreationTime { get; set; }
        
        public int ShipperId { get; set; }
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string ShipperName { get; set; }
        
        public int ShopId { get; set; }
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string ShopName { get; set; }
        
        public int ProductsInOrderCount { get; set; }
    }
}