using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO.DomainLikeDTO
{
    public class Category : DomainEntity
    {        
        [MaxLength(64,ErrorMessageResourceName = "ErrorMessageMaxLength" ,ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(CategoryName), ResourceType = typeof(Resources.Domain.Category))]
        public string CategoryName { get; set; }
        
        [Display(Name = nameof(Shop), ResourceType = typeof(Resources.Domain.Category))]
        public int? ShopId { get; set; }
        [Display(Name = nameof(Shop), ResourceType = typeof(Resources.Domain.Category))]
        public Shop Shop { get; set; }
        
       public ICollection<ProductInCategory> ProductsInCategory { get; set; }
    }
}