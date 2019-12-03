using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Identity;

namespace Domain
{
    public class Shop : DomainEntity
    {
        public int ShopShopNameId { get; set; }
        public int ShopShopAddressId{ get; set; }
        public int ShopShopContactId { get; set; }
        public int ShopShopContact2Id { get; set; }
        
        [ForeignKey(nameof(ShopShopNameId))]
        public MultiLangString ShopName { get; set; }
        
        [ForeignKey(nameof(ShopShopAddressId))]
        public MultiLangString ShopAddress { get; set; }
        
        [ForeignKey(nameof(ShopShopContactId))]
        public MultiLangString ShopContact { get; set; }
        
        [ForeignKey(nameof(ShopShopContact2Id))]
        public MultiLangString ShopContact2 { get; set; }
        
        public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
        public ICollection<Return> Returns { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
        public ICollection<Defect> Defects { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}