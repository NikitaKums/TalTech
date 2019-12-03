using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO
{
    public class Inventory
    {
        public int Id { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string Description { get; set; }
        
        [Required]
        public DateTime InventoryCreationTime { get; set; }
        
        public int ShopId { get; set; }
    }
}