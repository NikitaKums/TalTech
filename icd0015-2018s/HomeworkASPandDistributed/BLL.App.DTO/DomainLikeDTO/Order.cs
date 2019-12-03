using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO.DomainLikeDTO
{
    public class Order : DomainEntity
    {
        [MaxLength(128, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.Domain.Order))]
        public string Description { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(OrderCreationTime), ResourceType = typeof(Resources.Domain.Order))]
        [DataType(DataType.DateTime)]
        public DateTime OrderCreationTime { get; set; }

        public int ShipperId { get; set; }

        [Display(Name = nameof(Shipper), ResourceType = typeof(Resources.Domain.Order))]
        public Shipper Shipper { get; set; }

        public int ShopId { get; set; }

        [Display(Name = nameof(Shop), ResourceType = typeof(Resources.Domain.Order))]
        public Shop Shop { get; set; }

        public ICollection<ProductInOrder> ProductsInOrder { get; set; }
    }
}