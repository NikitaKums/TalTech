using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.App.DTO.DomainLikeDTO.Identity;

namespace DAL.App.DTO.DomainLikeDTO
{
    public class Sale : DomainEntity
    {
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string Description { get; set; }
        
        [Required]
        public DateTime SaleInitialCreationTime { get; set; }
        
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        
        public decimal? AllTotalSaleAmount { get; set; }
        public decimal? TodayTotalSaleAmount { get; set; }
        
        public ICollection<ProductSold> ProductsSold { get; set; }
    }
}