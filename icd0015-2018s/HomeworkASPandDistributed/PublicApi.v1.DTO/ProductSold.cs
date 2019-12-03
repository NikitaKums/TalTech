using System;
using System.ComponentModel.DataAnnotations;
using PublicApi.v1.DTO.IdAndNameDTO;

namespace PublicApi.v1.DTO
{
    public class ProductSold : ProductSoldProductSaleIdName
    {        
        [Range(1, int.MaxValue)]
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public DateTime ProductSoldTime { get; set; }        
    }
}