using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO.IdAndNameDTO
{
    public class ProductIdName
    {
        public int Id { get; set; }
        
        [MaxLength(256)]
        [MinLength(1)]
        [Required]
        public string ProductName { get; set; }
        
        public int? ProductInCategoryId { get; set; }
        public int? ProductWithDefectId { get; set; }
        public int? ProductReturnedId { get; set; }
        public int? ProductSoldId { get; set; }
    }
}