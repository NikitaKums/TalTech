using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO.DomainLikeDTO
{
    public class Return : DomainEntity
    {
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string Description { get; set; }
        
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        
        public ICollection<ProductReturned> ProductsReturned { get; set; }
    }
}