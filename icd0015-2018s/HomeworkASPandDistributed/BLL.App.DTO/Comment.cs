using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.IdAndNameDTO;

namespace BLL.App.DTO
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