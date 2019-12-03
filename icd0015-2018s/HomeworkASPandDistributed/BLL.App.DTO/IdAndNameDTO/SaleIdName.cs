using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO.IdAndNameDTO
{
    public class SaleIdName
    {
        public int Id { get; set; }
        [MaxLength(256)]
        [MinLength(1)]
        [Required]
        public string SaleDescription { get; set; }    }
}