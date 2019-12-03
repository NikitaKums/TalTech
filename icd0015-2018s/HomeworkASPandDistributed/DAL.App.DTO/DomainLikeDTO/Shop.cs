using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace DAL.App.DTO.DomainLikeDTO
{
    public class Shop : DomainEntity
    {
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string ShopName { get; set; }
        
        public string ShopAddress { get; set; }
        public string ShopContact { get; set; }
        public string ShopContact2 { get; set; }
        
        /*public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
        public ICollection<Return> Returns { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
        public ICollection<Defect> Defects { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Category> Categories { get; set; }*/
    }
}