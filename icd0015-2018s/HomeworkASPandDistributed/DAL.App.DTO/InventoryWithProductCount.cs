using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO
{
    public class InventoryWithProductCount
    {
        public int Id { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string Description { get; set; }
        
        [Required]
        public DateTime InventoryCreationTime { get; set; }
        
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public int ProductCount { get; set; }
    }
}