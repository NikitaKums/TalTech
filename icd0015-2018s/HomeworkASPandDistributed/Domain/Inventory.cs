using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Inventory : DomainEntity
    {
        public int InventoryDescriptionId { get; set; }
        
        [ForeignKey(nameof(InventoryDescriptionId))]
        public MultiLangString Description { get; set; }
        
        [Required]
        public DateTime InventoryCreationTime { get; set; }
        
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        
        public ICollection<Product> Products { get; set; }
    }
}