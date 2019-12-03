using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO.DomainLikeDTO
{
    public class Comment : DomainEntity
    {
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessageMaxLength" ,ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(CommentTitle), ResourceType = typeof(Resources.Domain.Comment))]
        public string CommentTitle { get; set; }
        
        [MaxLength(128, ErrorMessageResourceName = "ErrorMessageMaxLength" ,ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessageMinLength", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources.Domain.Common))]
        [Display(Name = nameof(CommentBody), ResourceType = typeof(Resources.Domain.Comment))]
        public string CommentBody { get; set; }
        
        public int ProductId { get; set; }
        [Display(Name = nameof(Product), ResourceType = typeof(Resources.Domain.Comment))]
        public Product Product { get; set; }
        
        public int ShopId { get; set; } 
        [Display(Name = nameof(Shop), ResourceType = typeof(Resources.Domain.Comment))]
        public Shop Shop { get; set; }
    }
}