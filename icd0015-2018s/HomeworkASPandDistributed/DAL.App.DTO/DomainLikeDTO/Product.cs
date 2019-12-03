using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO.DomainLikeDTO
{
    public class Product : DomainEntity
    {
        [MaxLength(512)]
        [MinLength(1)]
        [Required]
        public string ManuFacturerItemCode { get; set; }
        
        [MaxLength(512)]
        [MinLength(1)]
        [Required]
        public string ShopCode { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        [Required]
        public string ProductName { get; set; }
        
        [Range(1, int.MaxValue)]
        [Required]
        public decimal BuyPrice { get; set; }
        
        [Range(0, int.MaxValue)]
        [Required]
        public int PercentageAddedToBuyPrice { get; set; }
        
        [Range(1, int.MaxValue)]
        [Required]
        public decimal? SellPrice { get; set; }
        
        [Range(0, int.MaxValue)]
        [Required]
        public int Quantity { get; set; }
       
        [MaxLength(128)]
        public string Weight { get; set; } // string because can specify the measurement unit on insert
        
        [MaxLength(128)]
        public string Length { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
        
        public int ManuFacturerId { get; set; }
        public ManuFacturer Manufacturer { get; set; }
                
        public int? InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        
        /*public ICollection<ProductInCategory> ProductsInCategory { get; set; }
        public ICollection<ProductInOrder> ProductsInOrder { get; set; }
        public ICollection<ProductReturned> ProductsReturned { get; set; }
        public ICollection<ProductSold> ProductsSold { get; set; }
        public ICollection<ProductWithDefect> ProductsWithDefect { get; set; }*/

        public string ProductNameCode => ProductName + " " + ShopCode;

    }
}