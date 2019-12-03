using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO
{
    public class Category
    {
        public int Id { get; set; }
        public int? ShopId { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string CategoryName { get; set; }
    }
}