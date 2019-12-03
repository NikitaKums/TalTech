using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.nikita.Contracts.DAL.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : IdentityUser<int>, IDomainEntity
// PK type is int
    {
        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string FirstName { get; set; }
        
        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string LastName { get; set; }
        
        public string Aadress { get; set; }

        public string FirstLastName => FirstName + " " + LastName;
        
        public int? ShopId { get; set; }
        public Shop Shop { get; set; }
        
        public ICollection<Sale> Sales { get; set; }
    }
}