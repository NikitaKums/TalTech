using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO.IdAndNameDTO
{
    public class CommentIdTitleBody
    {
        public int Id { get; set; }
        
        [MaxLength(256)]
        [MinLength(1)]
        [Required]
        public string CommentTitle { get; set; }
        [MaxLength(256)]
        [MinLength(1)]
        [Required]
        public string CommentBody { get; set; }
    }
}