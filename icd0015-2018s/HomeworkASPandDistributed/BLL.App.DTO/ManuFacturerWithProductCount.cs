using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO
{
    public class ManuFacturerWithProductCount
    {
        public int Id { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string ManuFacturerName { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string Aadress { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string PhoneNumber { get; set; }

        public int ProductCount { get; set; }
    }
}