using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO.DomainLikeDTO
{
    public class Inventory : DomainEntity
    {
        // there is only one inventory | will have to limit later | ASPNET
        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string Description { get; set; }
        
        [Required]
        public DateTime InventoryCreationTime { get; set; }
        
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        
        //public ICollection<Product> Products { get; set; }
    }
}