using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO.IdAndNameDTO
{
    public class CategoryIdName
    {
        [MaxLength(256)]
        [MinLength(1)]
        [Required]
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}