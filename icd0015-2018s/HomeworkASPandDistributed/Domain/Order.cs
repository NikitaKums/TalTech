using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Order : DomainEntity
    {
        public int OrderDescriptionId { get; set; }
        
        [ForeignKey(nameof(OrderDescriptionId))]
        public MultiLangString Description { get; set; }
        
        [Required]
        public DateTime OrderCreationTime { get; set; }
        
        public int ShipperId { get; set; }
        public Shipper Shipper { get; set; }
        
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        
        public ICollection<ProductInOrder> ProductsInOrder { get; set; }
    }
}