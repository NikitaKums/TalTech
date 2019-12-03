using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO.DomainLikeDTO
{
    public class ProductReturned : DomainEntity
    {
        [Range(1, int.MaxValue)]
        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime ProductReturnedTime { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        public int ReturnId { get; set; }
        public Return Return { get; set; }
    }
}