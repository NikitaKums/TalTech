using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO
{
    public class AppUserShop
    {
        public int? ShopId { get; set; }
        
        [MaxLength(256)]
        [MinLength(1)]
        [Required]
        public string ShopName { get; set; }
    }
}