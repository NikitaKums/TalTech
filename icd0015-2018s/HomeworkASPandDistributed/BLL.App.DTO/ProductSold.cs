using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.IdAndNameDTO;

namespace BLL.App.DTO
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