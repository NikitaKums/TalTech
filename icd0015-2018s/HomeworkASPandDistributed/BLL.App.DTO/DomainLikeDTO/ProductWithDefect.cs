using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO.DomainLikeDTO
{
    public class ProductWithDefect : DomainEntity
    {
        [Range(1, int.MaxValue, ErrorMessageResourceName = "ErrorPositiveNumberRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(Quantity), ResourceType = typeof(Resources.Domain.ProductWithDefect))]
        public int Quantity { get; set; }
        
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(DefectRecordingTime), ResourceType = typeof(Resources.Domain.ProductWithDefect))]
        [DataType(DataType.DateTime)]
        public DateTime DefectRecordingTime { get; set; }
        
        public int ProductId { get; set; }
        [Display(Name = nameof(Product), ResourceType = typeof(Resources.Domain.ProductWithDefect))]
        public Product Product { get; set; }
        
        public int DefectId { get; set; }
        [Display(Name = nameof(Defect), ResourceType = typeof(Resources.Domain.ProductWithDefect))]
        public Defect Defect { get; set; }
    }
}