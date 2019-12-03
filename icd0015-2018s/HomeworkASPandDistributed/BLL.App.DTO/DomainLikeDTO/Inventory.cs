using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO.DomainLikeDTO
{
    public class Inventory : DomainEntity
    {
        // there is only one inventory | will have to limit later | ASPNET
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.Domain.Inventory))]
        public string Description { get; set; }
        
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(InventoryCreationTime), ResourceType = typeof(Resources.Domain.Inventory))]
        [DataType(DataType.DateTime)]
        public DateTime InventoryCreationTime { get; set; }
        
        public int ShopId { get; set; }
        [Display(Name = nameof(Shop), ResourceType = typeof(Resources.Domain.Inventory))]
        public Shop Shop { get; set; }
        
        //public ICollection<Product> Products { get; set; }
    }
}