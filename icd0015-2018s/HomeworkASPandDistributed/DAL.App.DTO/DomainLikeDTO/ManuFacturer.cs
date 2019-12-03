using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO.DomainLikeDTO
{
    public class ManuFacturer : DomainEntity
    {        
        [MaxLength(64)]
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
        
        //public ICollection<Product> Products { get; set; }
    }
}