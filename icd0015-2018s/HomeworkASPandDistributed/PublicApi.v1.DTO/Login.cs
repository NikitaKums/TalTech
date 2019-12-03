using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO
{
    public class Login
    {
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string Email { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string Password { get; set; }  
    }
}