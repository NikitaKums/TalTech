using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO.Identity
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