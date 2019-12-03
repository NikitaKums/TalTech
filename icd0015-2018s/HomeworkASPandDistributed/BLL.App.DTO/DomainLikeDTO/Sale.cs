using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.DomainLikeDTO.Identity;

namespace BLL.App.DTO.DomainLikeDTO
{
    public class Sale : DomainEntity
    {
        [MaxLength(128, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.Domain.Sale))]
        public string Description { get; set; }
        
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(SaleInitialCreationTime), ResourceType = typeof(Resources.Domain.Sale))]
        [DataType(DataType.DateTime)]
        public DateTime SaleInitialCreationTime { get; set; }
        
        public int AppUserId { get; set; }
        [Display(Name = nameof(AppUser), ResourceType = typeof(Resources.Domain.Sale))]
        public AppUser AppUser { get; set; }
        
        public decimal? AllTotalSaleAmount { get; set; }
        public decimal? TodayTotalSaleAmount { get; set; }
        
        public ICollection<ProductSold> ProductsSold { get; set; }
    }
}