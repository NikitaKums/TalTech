using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO.Identity
{
    public class AppUser : AppUserShop
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
        
        public string Address { get; set; }
        
    }
}