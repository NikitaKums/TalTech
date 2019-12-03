using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Identity;

namespace Domain
{
    public class Sale : DomainEntity
    {
        public int SaleDescriptionId { get; set; }
        
        [ForeignKey(nameof(SaleDescriptionId))]
        public MultiLangString Description { get; set; }
        
        [Required]
        public DateTime SaleInitialCreationTime { get; set; }
        
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        
        public ICollection<ProductSold> ProductsSold { get; set; }
    }
}