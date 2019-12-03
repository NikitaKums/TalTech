using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO.IdAndNameDTO
{
    public class CategoryIdName
    {
        public int Id { get; set; }
        
        [MaxLength(256)]
        [MinLength(1)]
        [Required]
        public string CategoryName { get; set; }
    }
}