using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO
{
    public class ProductInCategory
    {
        public int Id { get; set; }
        
        public int CategoryId { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string CategoryName { get; set; }
        
        public int ProductId { get; set; }
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string ProductName { get; set; }
    }
}