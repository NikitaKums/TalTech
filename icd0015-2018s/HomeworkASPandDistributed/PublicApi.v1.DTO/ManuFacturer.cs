using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO
{
    public class ManuFacturer
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
    }
}