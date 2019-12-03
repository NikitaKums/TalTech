using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO.DomainLikeDTO
{
    public class Comment : DomainEntity
    {
        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string CommentTitle { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string CommentBody { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}