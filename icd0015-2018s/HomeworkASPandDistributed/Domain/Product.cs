using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain
{
    public class Product : DomainEntity
    {
        public int ProductManuFacturerItemCodeId { get; set; }
        public int ProductShopCodeId { get; set; }
        public int ProductProductNameId { get; set; }
        public int ProductWeightId { get; set; }
        public int ProductLengthId { get; set; }

        [ForeignKey(nameof(ProductManuFacturerItemCodeId))]
        public MultiLangString ManuFacturerItemCode { get; set; }
        
        [ForeignKey(nameof(ProductShopCodeId))]
        public MultiLangString ShopCode { get; set; }
        
        [ForeignKey(nameof(ProductProductNameId))]
        public MultiLangString ProductName { get; set; }
        
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
       
        [ForeignKey(nameof(ProductWeightId))]
        public MultiLangString Weight { get; set; } // string because can specify the measurement unit on insert
        
        [ForeignKey(nameof(ProductLengthId))]
        public MultiLangString Length { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
        
        public int ManuFacturerId { get; set; }
        public ManuFacturer Manufacturer { get; set; }
                
        public int? InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        
        public ICollection<ProductInCategory> ProductsInCategory { get; set; }
        public ICollection<ProductInOrder> ProductsInOrder { get; set; }
        public ICollection<ProductReturned> ProductsReturned { get; set; }
        public ICollection<ProductSold> ProductsSold { get; set; }
        public ICollection<ProductWithDefect> ProductsWithDefect { get; set; }

       // public string ProductNameCode => ProductName + " " + ShopCode;

    }
}