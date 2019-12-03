using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO.DomainLikeDTO
{
    public class ProductSold : DomainEntity
    {      
        [Range(1, int.MaxValue, ErrorMessageResourceName = "ErrorPositiveNumberRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(Quantity), ResourceType = typeof(Resources.Domain.ProductSold))]
        public int Quantity { get; set; }
        
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(ProductSoldTime), ResourceType = typeof(Resources.Domain.ProductSold))]
        [DataType(DataType.DateTime)]
        public DateTime ProductSoldTime { get; set; }
        
        public int ProductId { get; set; }
        [Display(Name = nameof(Product), ResourceType = typeof(Resources.Domain.ProductSold))]
        public Product Product { get; set; }
                
        public int SaleId { get; set; }
        [Display(Name = nameof(Sale), ResourceType = typeof(Resources.Domain.ProductSold))]
        public Sale Sale { get; set; }
        
        public decimal? SaleAmount { get; set; }
    }
}