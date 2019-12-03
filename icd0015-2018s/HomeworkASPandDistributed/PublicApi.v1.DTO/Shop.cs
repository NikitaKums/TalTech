using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO
{
    public class Shop
    {
        public int Id { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string ShopName { get; set; }    
        public string ShopAddress { get; set; }
        public string ShopContact { get; set; }
        public string ShopContact2 { get; set; }
        
        public int InventoryId { get; set; }
        public int OrdersCount { get; set; }
        public int ProductsCount { get; set; }
        public int ReturnsCount { get; set; }
        public int DefectsCount { get; set; }
        public int AppUsersCount { get; set; }
        
        //public List<AppUserDTO> AppUserDTOs { get; set; }
    }
}