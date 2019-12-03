using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO
{
    public class Order
    {
        public int Id { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string Description { get; set; }
        
        [Required]
        public DateTime OrderCreationTime { get; set; }
        
        public int ShipperId { get; set; }
        public string ShipperName { get; set; }
        
        public int ShopId { get; set; }
        public string ShopName { get; set; }
    }
}