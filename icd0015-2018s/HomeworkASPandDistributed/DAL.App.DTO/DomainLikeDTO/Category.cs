using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO.DomainLikeDTO
{
    public class Category : DomainEntity
    {        
        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string CategoryName { get; set; }
        
        public int? ShopId { get; set; }
        public Shop Shop { get; set; }
        
        public ICollection<ProductInCategory> ProductsInCategory { get; set; }
    }
}