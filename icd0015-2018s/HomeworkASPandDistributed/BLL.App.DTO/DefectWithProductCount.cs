using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO
{
    public class DefectWithProductCount
    {
        public int Id { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string Description { get; set; }

        public int ShopId { get; set; }
        
        [MaxLength(256)]
        [MinLength(1)]
        [Required]
        public string ShopName { get; set; }
        public int ProductsWithDefectCount { get; set; }
    }
}