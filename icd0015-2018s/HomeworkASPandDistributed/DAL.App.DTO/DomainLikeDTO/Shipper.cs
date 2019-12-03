using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO.DomainLikeDTO
{
    public class Shipper : DomainEntity
    {
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
        
        //public ICollection<Order> Orders { get; set; }
    }
}