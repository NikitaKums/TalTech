using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO.DomainLikeDTO
{
    public class Order : DomainEntity
    {
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string Description { get; set; }
        
        [Required]
        public DateTime OrderCreationTime { get; set; }
        
        public int ShipperId { get; set; }
        public Shipper Shipper { get; set; }
        
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        
        
        public ICollection<ProductInOrder> ProductsInOrder { get; set; }
    }
}