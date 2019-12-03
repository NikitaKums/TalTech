using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO
{
    public class Defect
    {
        public int Id { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string Description { get; set; }

        public int ShopId { get; set; }
        public string ShopName { get; set; }
        
        public int ProductsWithDefectCount { get; set; }
    }
}