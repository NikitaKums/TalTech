using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO
{
    public class Shipper
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
    }
}