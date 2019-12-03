using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO.IdAndNameDTO
{
    public class ShopIdName
    {
        public int Id { get; set; }
        [MaxLength(256)]
        [MinLength(1)]
        [Required]
        public string ShopName { get; set; }    }
}