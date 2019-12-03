using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.IdAndNameDTO;

namespace BLL.App.DTO
{
    public class ReturnWithProductCount
    {
        public int Id { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string Description { get; set; }
        
        public int ShopId { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string ShopName { get; set; }
        
        public int ProductsReturnedCount { get; set; }
        public List<ProductIdName> ProductIdNameDtos { get; set; }
    }
}