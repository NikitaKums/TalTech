using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO.DomainLikeDTO.Identity
{
    public class AppUser
    {
        public int Id { get; set; }
        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string FirstName { get; set; }
        
        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string LastName { get; set; }
        
        public string Aadress { get; set; }
        
        public string Email { get; set; }

        public string FirstLastName => FirstName + " " + LastName;
        
        public int? ShopId { get; set; }
        public Shop Shop { get; set; }
        
        //public ICollection<Sale> Sales { get; set; }
    }
}