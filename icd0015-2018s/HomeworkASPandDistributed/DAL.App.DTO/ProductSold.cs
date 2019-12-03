using System;
using System.ComponentModel.DataAnnotations;
using DAL.App.DTO.IdAndNameDTO;

namespace DAL.App.DTO
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