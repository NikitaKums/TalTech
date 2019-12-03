using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ProductWithDefect : DomainEntity
    {
        [Range(1, int.MaxValue)]
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public DateTime DefectRecordingTime { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        public int DefectId { get; set; }
        public Defect Defect { get; set; }
    }
}