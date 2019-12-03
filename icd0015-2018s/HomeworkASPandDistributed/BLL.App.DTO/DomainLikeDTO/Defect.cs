using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO.DomainLikeDTO
{
    public class Defect : DomainEntity
    {        
        [MaxLength(128, ErrorMessageResourceName = "ErrorMessageMaxLength" ,ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.Domain.Defect))]
        public string Description { get; set; }
        
        public int ShopId { get; set; }
        [Display(Name = nameof(Shop), ResourceType = typeof(Resources.Domain.Comment))]
        public Shop Shop { get; set; }
        
        public ICollection<ProductWithDefect> ProductsWithDefect { get; set; }
    }
}