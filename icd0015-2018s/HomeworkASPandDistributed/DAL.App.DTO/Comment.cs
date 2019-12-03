using System.ComponentModel.DataAnnotations;
using DAL.App.DTO.IdAndNameDTO;

namespace DAL.App.DTO
{
    public class Comment : CommentIdTitleBody
    {
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string ProductName { get; set; }
        
        public int ProductId { get; set; }
        public int ShopId { get; set; }
    }
}