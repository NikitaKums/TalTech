using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO.DomainLikeDTO
{
    public class ProductInOrder : DomainEntity
    {
        [Range(1, int.MaxValue, ErrorMessageResourceName = "ErrorPositiveNumberRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(Quantity), ResourceType = typeof(Resources.Domain.ProductInOrder))]
        public int Quantity { get; set; }
        
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(ProductInOrderPlacingTime), ResourceType = typeof(Resources.Domain.ProductInOrder))]
        [DataType(DataType.DateTime)]
        public DateTime ProductInOrderPlacingTime { get; set; }
    
        public int ProductId { get; set; }
        [Display(Name = nameof(Product), ResourceType = typeof(Resources.Domain.ProductInOrder))]
        public Product Product { get; set; }
        
        public int OrderId { get; set; }
        [Display(Name = nameof(Order), ResourceType = typeof(Resources.Domain.ProductInOrder))]
        public Order Order { get; set; }
    }
}