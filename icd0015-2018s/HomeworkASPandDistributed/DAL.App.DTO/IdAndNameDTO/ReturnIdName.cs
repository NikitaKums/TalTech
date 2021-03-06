using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO.IdAndNameDTO
{
    public class ReturnIdName
    {
        public int Id { get; set; }
        [MaxLength(256)]
        [MinLength(1)]
        [Required]
        public string ReturnDescription { get; set; }    }
}