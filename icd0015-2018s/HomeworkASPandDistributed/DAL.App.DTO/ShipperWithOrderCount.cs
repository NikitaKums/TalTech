using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO
{
    public class ShipperWithOrderCount
    {
        public int Id { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string ShipperName { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string ShipperAddress { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string PhoneNumber { get; set; }

        public int OrdersCount { get; set; }
    }
}