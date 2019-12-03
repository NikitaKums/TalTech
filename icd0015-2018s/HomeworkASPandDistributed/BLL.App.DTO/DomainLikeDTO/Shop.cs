using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO.DomainLikeDTO
{
    public class Shop : DomainEntity
    {
        [MaxLength(128, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(ShopName), ResourceType = typeof(Resources.Domain.Shop))]
        public string ShopName { get; set; }
        
        [Display(Name = nameof(ShopAddress), ResourceType = typeof(Resources.Domain.Shop))]
        public string ShopAddress { get; set; }
        [Display(Name = nameof(ShopContact), ResourceType = typeof(Resources.Domain.Shop))]
        public string ShopContact { get; set; }
        [Display(Name = nameof(ShopContact2), ResourceType = typeof(Resources.Domain.Shop))]
        public string ShopContact2 { get; set; }
        
        /*public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
        public ICollection<Return> Returns { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
        public ICollection<Defect> Defects { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Category> Categories { get; set; }*/
    }
}