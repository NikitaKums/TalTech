using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO.DomainLikeDTO
{
    public class Product : DomainEntity
    {
        [MaxLength(512, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(ManuFacturerItemCode), ResourceType = typeof(Resources.Domain.Product))]
        public string ManuFacturerItemCode { get; set; }
        
        [MaxLength(512, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(ShopCode), ResourceType = typeof(Resources.Domain.Product))]
        public string ShopCode { get; set; }
        
        [MaxLength(128, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(ProductName), ResourceType = typeof(Resources.Domain.Product))]
        public string ProductName { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessageResourceName = "ErrorPositiveNumberRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(BuyPrice), ResourceType = typeof(Resources.Domain.Product))]
        public decimal BuyPrice { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessageResourceName = "ErrorPositiveNumberRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(PercentageAddedToBuyPrice), ResourceType = typeof(Resources.Domain.Product))]
        public int PercentageAddedToBuyPrice { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessageResourceName = "ErrorPositiveNumberRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(SellPrice), ResourceType = typeof(Resources.Domain.Product))]
        public decimal? SellPrice { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessageResourceName = "ErrorPositiveNumberRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(Quantity), ResourceType = typeof(Resources.Domain.Product))]
        public int Quantity { get; set; }
       
        [MaxLength(128, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(Weight), ResourceType = typeof(Resources.Domain.Product))]
        public string Weight { get; set; } // string because can specify the measurement unit on insert
        
        [MaxLength(128, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(Length), ResourceType = typeof(Resources.Domain.Product))]
        public string Length { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
        
        public int ManuFacturerId { get; set; }
        [Display(Name = nameof(Manufacturer), ResourceType = typeof(Resources.Domain.Product))]
        public ManuFacturer Manufacturer { get; set; }
                
        public int? InventoryId { get; set; }
        [Display(Name = nameof(Inventory), ResourceType = typeof(Resources.Domain.Product))]
        public Inventory Inventory { get; set; }
        
        public int ShopId { get; set; }
        [Display(Name = nameof(Shop), ResourceType = typeof(Resources.Domain.Product))]
        public Shop Shop { get; set; }
        
        /*public ICollection<ProductInCategory> ProductsInCategory { get; set; }
        public ICollection<ProductInProduct> ProductsInOrder { get; set; }
        public ICollection<ProductReturned> ProductsReturned { get; set; }
        public ICollection<ProductSold> ProductsSold { get; set; }
        public ICollection<ProductWithDefect> ProductsWithDefect { get; set; }*/

        //public string ProductNameCode => ProductName + " " + ShopCode;

    }
}