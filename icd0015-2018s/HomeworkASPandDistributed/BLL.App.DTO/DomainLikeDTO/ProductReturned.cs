using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO.DomainLikeDTO
{
    public class ProductReturned : DomainEntity
    {
        [Range(1, int.MaxValue, ErrorMessageResourceName = "ErrorPositiveNumberRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(Quantity), ResourceType = typeof(Resources.Domain.ProductReturned))]
        public int Quantity { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(ProductReturnedTime), ResourceType = typeof(Resources.Domain.ProductReturned))]
        [DataType(DataType.DateTime)]
        public DateTime ProductReturnedTime { get; set; }
        
        public int ProductId { get; set; }
        [Display(Name = nameof(Product), ResourceType = typeof(Resources.Domain.ProductReturned))]
        public Product Product { get; set; }
        
        public int ReturnId { get; set; }
        [Display(Name = nameof(Return), ResourceType = typeof(Resources.Domain.ProductReturned))]
        public Return Return { get; set; }
    }
}