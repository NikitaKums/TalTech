using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PublicApi.v1.DTO.IdAndNameDTO;

namespace PublicApi.v1.DTO
{
    public class Return
    {
        public int Id { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string Description { get; set; }
        
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        
        public int ProductsReturnedCount { get; set; }
        public List<ProductIdName> ProductIdNameDtos { get; set; }
    }
}