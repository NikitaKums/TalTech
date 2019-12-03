using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PublicApi.v1.DTO.IdAndNameDTO;

namespace PublicApi.v1.DTO
{
    public class Sale
    {
        public int Id { get; set; }
        
        [MaxLength(256)]
        [MinLength(1)]
        [Required]
        public string Description { get; set; }
        
        [Required]
        public DateTime SaleInitialCreationTime { get; set; }
        
        public int ProductsSoldCount { get; set; }
        public int AppUserId { get; set; }
        public string AppUserName { get; set; }
        public string AppUserLastName { get; set; }
        
        public decimal? AllTotalSaleAmount { get; set; }
        public decimal? TodayTotalSaleAmount { get; set; }

        public List<ProductSoldProductSaleIdName> ProductsInSaleDTOs { get; set; }
    }
}