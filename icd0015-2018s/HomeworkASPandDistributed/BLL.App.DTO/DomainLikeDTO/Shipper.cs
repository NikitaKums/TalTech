using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO.DomainLikeDTO
{
    public class Shipper : DomainEntity
    {
        [MaxLength(128, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(ShipperName), ResourceType = typeof(Resources.Domain.Shipper))]
        public string ShipperName { get; set; }
        
        [MaxLength(128, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(ShipperAddress), ResourceType = typeof(Resources.Domain.Shipper))]
        public string ShipperAddress { get; set; }
        
        [MaxLength(128, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(PhoneNumber), ResourceType = typeof(Resources.Domain.Shipper))]
        public string PhoneNumber { get; set; }
        
        //public ICollection<Order> Orders { get; set; }
    }
}