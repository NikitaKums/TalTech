using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO
{
    public class ProductWithDefect
    {
        public int Id { get; set; }
        
        [Range(1, int.MaxValue)]
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public DateTime DefectRecordingTime { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        
        public int DefectId { get; set; }
        public string DefectDescription { get; set; }
    }
}