using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO.DomainLikeDTO
{
    public class ProductInCategory : DomainEntity
    {
        public int ProductId { get; set; }
        [Display(Name = nameof(Product), ResourceType = typeof(Resources.Domain.ProductInCategory))]
        public Product Product { get; set; }
        
        public int CategoryId { get; set; }
        [Display(Name = nameof(Category), ResourceType = typeof(Resources.Domain.ProductInCategory))]
        public Category Category { get; set; }
    }
}